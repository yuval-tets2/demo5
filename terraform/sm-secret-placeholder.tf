resource "random_password" "placeholder_secret_password" {
  length  = 20
  special = false
}

resource "aws_secretsmanager_secret" "secrets_placeholder" {
  name = "placeholder_secrets"
}

resource "aws_secretsmanager_secret_version" "secrets_version_placeholder" {
  secret_id     = aws_secretsmanager_secret.secrets_placeholder.id
  secret_string = jsonencode({
    BCRYPT_SALT       = "10"
    JWT_EXPIRATION    = "2d"
    JWT_SECRET_KEY    = random_password.placeholder_secret_password.result
    DB_URL            = "postgres://${module.rds_placeholder.db_instance_username}:${random_password.placeholder_database_password.result}@${module.rds_placeholder.db_instance_address}:5432/${module.rds_placeholder.db_instance_name}"
    DB_URL_DOTNET     = "Server=${module.rds_placeholder.db_instance_address};Port=5432;Database=${module.rds_placeholder.db_instance_name};User Id=${module.rds_placeholder.db_instance_username};Password=${random_password.placeholder_database_password.result};"
  })
}
