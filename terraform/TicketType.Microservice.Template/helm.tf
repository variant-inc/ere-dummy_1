// var.deployable is set in scripts/octo/plan.sh & delpoy.sh
resource "helm_release" "ticket_type_microservice_prototype" {
  chart           = "../../helm/${var.deployable}"
  name            = local.normalized_deploy_name
  namespace       = var.target_namespace
  lint            = true
  cleanup_on_fail = true

  // The following are set in scripts/octo/plan.sh & delpoy.sh:
  set {
    name  = "revision"
    value = var.revision
  }

  set {
    name  = "image.tag"
    value = var.image_tag
  }
  /// End

  set {
    name  = "forceRefresh"
    value = "1.1"
  }

  set {
    name  = "fullnameOverride"
    value = local.normalized_deploy_name
  }

  set {
    name  = "serviceAccount.name"
    value = var.k8s_serviceaccount
  }

  set {
    name  = "serviceAccount.roleArn"
    value = aws_iam_role.application_policy.arn
  }

  set {
    name  = "global.namespaceName"
    value = var.target_namespace
  }

  set {
    name  = "global.service.name"
    value = local.normalized_deploy_name
  }

  // Environment vars
  set {
    name  = "envVars.awsRegion"
    value = var.aws_default_region
  }

  set {
    name  = "envVars.SQS.QueueUrl"
    value = aws_sqs_queue.incoming_entity_queue.id
  }

  set {
    name  = "envVars.SNS.OutgoingTopicName"
    value = var.ticketing_api_outgoing_topic_name
  }

  set {
    name  = "envVars.SNS.OutgoingTopicArn"
    value = aws_sns_topic.outgoing_exceptions_topic.arn
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

  set {
    name  = "envVars.entityApiUserAgent"
    value = var.enity_api_user_agent
  }
  
  set {
    name  = "envVars.entityApiBaseAddress"
    value = var.entity_api_base_address
  }
  
  set {
    name  = "envVars.entityApiDriverPath"
    value = var.entity_api_driver_path
  }
  
  set {
    name  = "envVars.entityApiHometimePath"
    value = var.entity_api_hometime_path
  }
  
  set {
    name  = "envVars.entityApiOrderPath"
    value = var.entity_api_order_path
  }
  
  set {
    name  = "envVars.entityApiTractorPath"
    value = var.entity_api_tractor_path
  }
}