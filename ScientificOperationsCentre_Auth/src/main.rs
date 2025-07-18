use actix_cors::Cors;
use actix_web::{web, App, HttpServer, HttpResponse, Responder, post};
use sqlx::SqlitePool;
use argon2::{Argon2,PasswordVerifier};
use argon2::PasswordHash as PH;
use jsonwebtoken::{encode, EncodingKey, Header};
mod models;
use utoipa::OpenApi;
use utoipa_swagger_ui::SwaggerUi;
use std::env;
use models::{UserLogin, TokenResponse};


#[utoipa::path(
    post,
    path = "/auth/login",
    request_body = UserLogin,
    responses(
        (status = 200, description = "JWT Token", body = TokenResponse),
        (status = 401, description = "Unauthorized")
    )
)]
#[post("/auth/login")]
async fn login(
    user: web::Json<UserLogin>,
    db: web::Data<SqlitePool>,
) -> impl Responder {
    // Query user from database
    let row = sqlx::query!("SELECT password_hash FROM users WHERE username = ?", user.username)
        .fetch_optional(db.get_ref())
        .await;
    match row {
        Ok(Some(record)) => {
            let parsed_hash = PH::new(&record.password_hash);
            if let Ok(hash) = parsed_hash {
                let argon2 = Argon2::default();
                if argon2.verify_password(user.password.as_bytes(), &hash).is_ok() {
                    let claims = serde_json::json!({
                        "sub": user.username,
                        "exp": chrono::Utc::now().timestamp() + 8 * 3600,
                        "iss": "http://scientificoperationscentre.auth:8060"
                    });
                    let key = env::var("JWT_KEY").unwrap_or_else(|_| "my-secret-key-for-development-only".into());
                    let token = encode(&Header::default(), &claims, &EncodingKey::from_secret(key.as_bytes())).unwrap();
                    return HttpResponse::Ok().json(TokenResponse { token });
                }
            }
            HttpResponse::Unauthorized().body("Invalid username or password")
        }
        Ok(None) => HttpResponse::Unauthorized().body("Invalid username or password"),
        Err(_) => HttpResponse::InternalServerError().body("Database error"),
    }
}


#[derive(OpenApi)]
#[openapi(paths(login), components(schemas(UserLogin, TokenResponse)))]
struct ApiDoc;

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    dotenv::dotenv().ok();
    let db_url = std::env::var("DATABASE_URL").unwrap_or_else(|_| "sqlite:///app/users.db".to_string());
    let pool = SqlitePool::connect(&db_url).await.expect("Failed to connect to DB");
    HttpServer::new(move || {
        let cors = Cors::default()
            .allowed_origin("http://localhost:8000")
            .allowed_methods(vec!["POST", "OPTIONS"])
            .allow_any_header();
        App::new()
            .app_data(web::Data::new(pool.clone()))
            .wrap(cors)
            .service(login)
            .service(SwaggerUi::new("/swagger-ui/{_:.*}").url("/api-doc/openapi.json", ApiDoc::openapi()))
            .route("/", web::get().to(|| async { HttpResponse::Found().append_header(("Location", "/swagger-ui/")).finish() }))
    })
    .bind(("0.0.0.0", 8060))?
    .run()
    .await
}