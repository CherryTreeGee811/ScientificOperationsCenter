use serde::{Deserialize, Serialize};
use utoipa::ToSchema;


#[derive(Deserialize, Serialize, ToSchema)]
pub struct UserLogin {
    pub username: String,
    pub password: String,
}


#[derive(Serialize, ToSchema)]
pub struct TokenResponse {
    pub token: String,
}