FROM rust:alpine AS builder
WORKDIR /app
RUN apk add --no-cache musl-dev sqlite curl
RUN rustup target add x86_64-unknown-linux-musl

# Copy dependency files first and precompile dependencies
COPY Cargo.toml Cargo.lock ./
RUN mkdir src && echo "fn main() {}" > src/main.rs
RUN cargo build --release --target x86_64-unknown-linux-musl || true

# Copy the rest of the application code
COPY . .

# Create the database
ENV DATABASE_URL=sqlite:///app/users.db
RUN cargo install sqlx-cli --no-default-features --features sqlite
RUN sqlite3 /app/users.db ""
RUN sqlx migrate run --database-url sqlite:///app/users.db
RUN cargo sqlx prepare --database-url sqlite:///app/users.db

# Build the application & seed the database
RUN cargo build --release --target x86_64-unknown-linux-musl --bin ScientificOperationsCentre_Auth --bin seed_users
RUN /app/target/x86_64-unknown-linux-musl/release/seed_users

FROM alpine:latest
WORKDIR /app
COPY --from=builder /app/target/x86_64-unknown-linux-musl/release/ScientificOperationsCentre_Auth /app/
COPY --from=builder /app/users.db /app/
EXPOSE 8060
ENTRYPOINT ["/app/ScientificOperationsCentre_Auth"]