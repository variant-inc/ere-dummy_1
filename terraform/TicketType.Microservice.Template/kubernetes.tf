resource "kubernetes_namespace" "ticket_type_microservice_template_k8_namespace" {
  metadata {
    name = var.TARGET_NAMESPACE
    labels = {
      "istio-injection": "enabled"
    }
  }
}