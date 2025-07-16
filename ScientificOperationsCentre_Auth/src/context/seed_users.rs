use sqlx::sqlite::SqlitePoolOptions;
use argon2::{Argon2, PasswordHasher};
use argon2::password_hash::SaltString;
use argon2::password_hash::rand_core::OsRng;


#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    let db_url = std::env::var("DATABASE_URL").unwrap_or_else(|_| "sqlite:///app/users.db".to_string());
    let pool = SqlitePoolOptions::new()
        .max_connections(1)
        .connect(&db_url)
        .await?;

    let app_env = std::env::var("APP_ENV").unwrap_or_else(|_| "development".to_string());
    let mut users = Vec::new();

    if app_env == "production" {
        let ground_control_pass = std::env::var("GROUND_CONTROL_PASS")
            .expect("GROUND_CONTROL_PASS must be set in production");
        users.push(("ground_control_sa".to_string(), ground_control_pass.to_string()));
    } else if app_env == "development" {
        users.push(("sciops_test".to_string(), "Hello123*".to_string()));
    }

    let argon2 = Argon2::default();
    for (username, password) in users {
        let salt = SaltString::generate(&mut OsRng);
        let password_hash = argon2
            .hash_password(password.as_bytes(), &salt)
            .map_err(|e| format!("Hash error: {e}"))?
            .to_string();
        sqlx::query(
            "INSERT OR IGNORE INTO users (username, password_hash) VALUES (?, ?)"
        )
        .bind(&username)
        .bind(&password_hash)
        .execute(&pool)
        .await?;
        println!("Seeded user: {}", username);
    }
    Ok(())
}