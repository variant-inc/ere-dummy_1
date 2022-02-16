terraform {
  backend "s3" {}
  required_providers {
    aws = {
      version = "~> 3.49.0"
    }
    helm = {
      version = "~> 2.2.0"
    }
    kubernetes = {
      version = "~> 2.3.2"
    }    
  }
}

provider "aws" {
  region = var.aws_default_region
}

provider "helm" {}

provider "kubernetes" {}

data "aws_caller_identity" "current" {}

data "aws_kms_alias" "sns" {
  name = var.kms_key_alias_sns
}

locals {
  aws_account_id    = data.aws_caller_identity.current.account_id
  project_name      = "exception-recognition-engine"
  deployable        = "ticket-type-microservice-prototype-v1"
  env_deployable    = "${var.environment}-${local.project_name}-${local.deployable}"
  env_name          = "${var.environment}-${local.deployable}"
  legacy_deployable = "TicketType.Microservice.Template"
  kebab_name        = replace(lower(local.legacy_deployable), ".", "-")
  kebab_env_name    = "${var.environment}-${local.kebab_name}"
}