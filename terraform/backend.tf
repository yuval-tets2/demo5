terraform {
  backend "s3" {
    bucket = "terraform-state-demonstration"
    key    = "development/placeholder"
    region = "us-east-1"
  }
}