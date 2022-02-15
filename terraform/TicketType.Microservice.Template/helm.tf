resource "helm_release" "ticket_type_microservice_prototype" {
  chart     = "../../helm/${local.legacy_deployable}"
  name      = local.deployable
  namespace = var.target_namespace
  lint      = true

  set {
    name  = "forceRefresh"
    value = "1.1"
  }

  set {
    name  = "fullnameOverride"
    value = local.deployable
  }

  set {
    name  = "image.tag"
    value = var.image_tag
  }

  set {
    name  = "serviceAccount.name"
    value = var.k8s_serviceaccount
  }

  set {
    name  = "serviceAccount.roleArn"
    value = aws_iam_role.ticket_type_microservice_template.arn
  }

  set {
    name  = "awsSecretName"
    value = data.aws_secretsmanager_secret.ticket_type_aws_secrets.name
  }

  set {
    name  = "awsSecretResourceName"
    value = var.aws_secret_resource_name
  }

  set {
    name  = "global.namespaceName"
    value = var.target_namespace
  }

  set {
    name  = "global.service.name"
    value = local.deployable
  }

  set {
    name  = "revision"
    value = var.revision
  }

  // Environment vars
  set {
    name  = "envVars.awsRegion"
    value = var.aws_default_region
  }

  set {
    name  = "envVars.launchDarkly.Key"
    value = var.launch_darkly_key
  }

  set {
    name  = "envVars.launchDarkly.UserName"
    value = var.launch_darkly_user
  }

  set {
    name  = "envVars.epsagonToken"
    value = var.epsagon_token
  }

  set {
    name  = "envVars.epsagonAppName"
    value = var.epsagon_app_name
  }
}