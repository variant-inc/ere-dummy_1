resource "aws_iam_role" "ticket_type_microservice_template" {
  name               = local.env_name
  description        = "IAM role for ${local.kebab_name} in ${var.environment} environment"
  assume_role_policy = data.aws_iam_policy_document.instance_assume_role_policy.json

  inline_policy {
    name = local.kebab_env_name
    policy = data.aws_iam_policy_document.ticket_type_microservice_template_policy_document.json
  }
}

data "aws_iam_policy_document" "ticket_type_microservice_template_policy_document" {
  version = local.aws_policy_version

#  statement {
#    effect = "Allow"
#  }
#
#  statement {
#    effect = "Allow"
#    resources = [
#      data.aws_kms_alias.sns.target_key_arn,
#    ]
#    actions = [
#      "kms:GenerateDataKey",
#      "kms:Decrypt"
#    ]
#  }
}

data "aws_iam_policy_document" "instance_assume_role_policy" {
  version = local.aws_policy_version
  statement {
    effect = "Allow"
    principals {
      identifiers = ["arn:aws:iam::${local.aws_account_id}:oidc-provider/${var.oidc_provider}"]
      type = "Federated"
    }
    actions = ["sts:AssumeRoleWithWebIdentity"]
    condition {
      test = "StringEquals"
      values = ["system:serviceaccount:${var.target_namespace}:${var.k8s_serviceaccount}"]
      variable = "${var.oidc_provider}:sub"
    }
  }
}
