{{/*
Expand the name of the chart.
*/}}
{{- define "TicketType.Microservice.Template.name" -}}
{{- .Release.Name }}
{{- end }}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "TicketType.Microservice.Template.fullname" -}}
{{- .Release.Name }}
{{- end }}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "TicketType.Microservice.Template.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Common labels
*/}}
{{- define "TicketType.Microservice.Template.labels" -}}
helm.sh/chart: {{ include "TicketType.Microservice.Template.chart" . }}
{{ include "TicketType.Microservice.Template.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Selector labels
*/}}
{{- define "TicketType.Microservice.Template.selectorLabels" -}}
app.kubernetes.io/name: {{ include "TicketType.Microservice.Template.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}

{{/*
Create the name of the service account to use
*/}}
{{- define "TicketType.Microservice.Template.serviceAccountName" -}}
{{- if .Values.serviceAccount.create }}
{{- default (include "TicketType.Microservice.Template.fullname" .) .Values.serviceAccount.name }}
{{- else }}
{{- default "default" .Values.serviceAccount.name }}
{{- end }}
{{- end }}
