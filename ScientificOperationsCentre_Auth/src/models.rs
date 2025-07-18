use serde::{Deserialize, Serialize};


#[derive(Deserialize, Serialize)]
pub struct UserLogin {
    pub username: String,
    pub password: String,
}


#[derive(Serialize)]
pub struct TokenResponse {
    pub token: String,
}