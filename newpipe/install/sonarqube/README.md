# Instalação do SonarQube com Helm

Este documento contém instruções para instalar o SonarQube usando Helm no cluster EKS com Traefik como Ingress Controller.

## Pré-requisitos

- Cluster EKS configurado
- Helm 3 instalado
- Traefik Ingress Controller instalado
- Certificado SSL válido no AWS Certificate Manager
- AWS Load Balancer Controller configurado

## Adicionar o repositório Helm

```bash
helm repo add sonarqube https://SonarSource.github.io/helm-chart-sonarqube
helm repo update
```

## Instalação

1. Crie o namespace para o SonarQube:

```bash
kubectl create namespace sonarqube
```

2. Crie o Secret com o certificado TLS:

```bash
kubectl create secret tls sonarqube-tls \
  --cert=/path/to/cert.pem \
  --key=/path/to/key.pem \
  -n sonarqube
```

3. Instale o SonarQube usando o arquivo values.yaml:

```bash
helm install sonarqube sonarqube/sonarqube -n sonarqube -f values.yaml
```

## Configurações

O arquivo `values.yaml` contém as seguintes configurações principais:

- Serviço: ClusterIP na porta 9000
- PostgreSQL como banco de dados
- Recursos: 500m CPU e 1Gi memória (requests)
- Persistência: 10Gi para dados e PostgreSQL
- Ingress configurado com Traefik e TLS
- Middleware de strip prefix configurado

## Ajustes Necessários

Antes de instalar, você precisa:

1. Substituir o domínio em `sonarqube.your-domain.com` pelo seu domínio real
2. Configurar o certificado TLS no AWS Certificate Manager
3. Criar o Secret com o certificado TLS
4. Verificar se o Traefik está configurado corretamente

## Acessando o SonarQube

Após a instalação, o SonarQube estará disponível em:

```
https://sonarqube.your-domain.com
```

Credenciais padrão:
- Usuário: admin
- Senha: admin

## Verificação

Após a instalação, verifique:

1. Os pods do SonarQube:
   ```bash
   kubectl get pods -n sonarqube
   ```

2. O serviço:
   ```bash
   kubectl get svc -n sonarqube
   ```

3. O Ingress:
   ```bash
   kubectl get ingress -n sonarqube
   ```

## Atualização

Para atualizar a instalação:

```bash
helm upgrade sonarqube sonarqube/sonarqube -n sonarqube -f values.yaml
```

## Desinstalação

Para remover o SonarQube:

```bash
helm uninstall sonarqube -n sonarqube
kubectl delete namespace sonarqube
```

## Notas Importantes

- O SonarQube está configurado para usar o Traefik como Ingress Controller
- O certificado TLS é gerenciado pelo AWS Certificate Manager
- O tráfego é roteado através do Load Balancer do Traefik
- Certifique-se de configurar corretamente os Security Groups
- Monitore os logs para identificar possíveis problemas 