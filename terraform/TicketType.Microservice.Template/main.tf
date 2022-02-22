terraform {
  backend "s3" {}
  required_providers {
    aws = {
      version = "~> 3.49.0"
    }
    helm = {
      version = "~> 1.3.0"
    }
    kubernetes = {
      version = "~> 1.13.0"
    }
  }
}

provider "aws" {
  region = var.aws_default_region
  default_tags {
    tags = module.tags.tags
  }
}

provider "helm" {}

provider "kubernetes" {}

data "aws_caller_identity" "current" {}

data "aws_kms_alias" "sns_key" {
  name = var.kms_key_alias_sns
}

# var.environment is set in scripts/octo/plan.sh & delpoy.sh:
locals {
  aws_account_id                = data.aws_caller_identity.current.account_id
  normalized_deploy_name        = replace(lower(var.deployable), ".", "-")
  env_deployable                = "${var.environment}-${var.project_name}-${local.normalized_deploy_name}"
  env_name                      = "${var.environment}-${local.normalized_deploy_name}"
  sns_ticketing_topic_name      = "${var.environment}-${var.ticketing_api_outgoing_topic_name}"
  sns_incoming_topic_name       = "${var.environment}-${var.entity_api_incoming_topic_name}"
  sns_incoming_topic_data_name  = "${var.environment}-${var.entity_api_incoming_topic_name}-data"
  sqs_incoming_queue_name       = "${var.environment}-${var.entity_api_incoming_queue_name}"
}