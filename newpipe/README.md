# Templates de CI/CD para Azure DevOps

Este repositório contém templates reutilizáveis para pipelines de CI/CD no Azure DevOps, incluindo build, testes, análise de código e deploy em múltiplos ambientes.

## Estrutura do Repositório

```
newpipe/
├── build/                 # Templates base de pipeline
│   └── base-pipeline.yaml # Template principal
├── charts/               # Templates Helm
│   ├── Chart.yaml
│   ├── values-dev.yaml
│   ├── values-hml.yaml
│   ├── values-prd.yaml
│   └── templates/
│       ├── deployment.yaml
│       ├── service.yaml
│       └── hpa.yaml
└── examples/             # Exemplos de uso
    └── sample-pipeline.yaml
```

## Como Usar

1. Copie o arquivo `examples/sample-pipeline.yaml` para a raiz do seu projeto
2. Renomeie para `azure-pipelines.yml`
3. Ajuste os parâmetros conforme necessário:

```yaml
extends:
  template: build/base-pipeline.yaml
  parameters:
    projectName: 'SeuProjeto.API'
    solutionPath: 'SeuProjeto.sln'
    dockerfile: 'Dockerfile'
    helmValuesPath: 'helm'
    runSonar: true
    sonarProject: 'seu-projeto'
    environments:
      - name: 'DEV'
        awsCredentials: 'AWS'
        region: 'us-east-1'
      - name: 'HML'
        awsCredentials: 'AWS-HML'
        region: 'us-east-1'
      - name: 'PRD'
        awsCredentials: 'AWS-PROD'
        region: 'sa-east-1'
```

## Funcionalidades

- Build e testes automatizados
- Análise de código com SonarQube
- Cobertura de testes
- Build e push de imagens Docker
- Deploy em múltiplos ambientes (DEV, HML, PRD)
- Deploy com Helm para Kubernetes
- Auto-scaling configurável
- Health checks
- Monitoramento de recursos

## Pré-requisitos

- Azure DevOps
- AWS ECR
- Kubernetes
- Helm
- SonarQube
- Docker

## Configuração

1. Configure as variáveis de grupo no Azure DevOps:
   - ValueDev
   - ValueHML
   - ValuePRD

2. Configure as credenciais AWS no Azure DevOps:
   - AWS (DEV)
   - AWS-HML (HML)
   - AWS-PROD (PRD)

3. Configure as conexões de serviço do Kubernetes:
   - K8S-DEV
   - K8S-HML
   - K8S-PRD

4. Configure o SonarQube no Azure DevOps

## Ambientes

### Desenvolvimento (DEV)
- 1 réplica
- Recursos limitados
- ClusterIP
- Auto-scaling: 1-3 réplicas

### Homologação (HML)
- 2 réplicas
- Recursos intermediários
- ClusterIP
- Auto-scaling: 2-5 réplicas

### Produção (PRD)
- 3 réplicas
- Recursos maiores
- LoadBalancer
- Auto-scaling: 3-10 réplicas 