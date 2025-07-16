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

    // Create users table if it doesn't exist
    sqlx::query(
        "CREATE TABLE IF NOT EXISTS users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL UNIQUE,
            password_hash TEXT NOT NULL
        );"
    ).execute(&pool).await?;

    // Users to insert
    let users = vec![
        ("sciops_test", "Hello123*"),
        ("ground_control_sa", "ExploreSpace223*"),
    ];
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
        .bind(username)
        .bind(password_hash)
        .execute(&pool)
        .await?;
        println!("Seeded user: {}", username);
    }
    Ok(())
}