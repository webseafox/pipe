# Instalação do Traefik Ingress Controller no EKS

Este documento contém instruções para instalar o Traefik Ingress Controller no Amazon EKS com suporte a Load Balancers internos e externos.

## Pré-requisitos

- Cluster EKS configurado
- AWS Load Balancer Controller instalado
- Helm 3 instalado
- Certificado SSL válido no AWS Certificate Manager (para o Load Balancer externo)
- IAM permissions configuradas para o AWS Load Balancer Controller

## Adicionar o repositório Helm

```bash
helm repo add traefik https://traefik.github.io/charts
helm repo update
```

## Instalação

1. Crie o namespace para o Traefik:

```bash
kubectl create namespace traefik
```

2. Instale o Traefik usando o arquivo values.yaml:

```bash
helm install traefik traefik/traefik -n traefik -f values.yaml
```

## Configuração de Fallback

Para configurar o roteamento de fallback para outro proxy:

1. Ajuste o arquivo `fallback-config.yaml`:
   - Substitua `outro-proxy.your-domain.com` pelo endereço do seu proxy de fallback
   - Ajuste os prefixos no middleware conforme necessário

2. Aplique a configuração de fallback:

```bash
kubectl apply -f fallback-config.yaml
```

## Configurações

O arquivo `values.yaml` contém as seguintes configurações principais:

- Dois replicas do Traefik
- Load Balancer externo (internet-facing) com SSL
- Load Balancer interno para tráfego VPC
- Configuração de recursos: 100m CPU e 128Mi memória (requests)
- Logs em formato JSON
- TLS habilitado
- Middlewares padrão configurados

O arquivo `fallback-config.yaml` contém:

- Roteador de fallback para tráfego não correspondente
- Serviço de fallback apontando para outro proxy
- Middleware de strip prefix para manipulação de rotas

## Ajustes Necessários

Antes de instalar, você precisa:

1. Substituir o ARN do certificado SSL no `values.yaml`:
   ```yaml
   service.beta.kubernetes.io/aws-load-balancer-ssl-cert: "arn:aws:acm:region:account:certificate/cert-id"
   ```

2. Verificar se o AWS Load Balancer Controller tem as permissões necessárias:
   - Criar Load Balancers
   - Gerenciar certificados ACM
   - Acessar recursos de rede

## Verificação

Após a instalação, verifique:

1. Os Load Balancers criados:
   ```bash
   aws elbv2 describe-load-balancers
   ```

2. Os pods do Traefik:
   ```bash
   kubectl get pods -n traefik
   ```

3. Os serviços:
   ```bash
   kubectl get svc -n traefik
   ```

4. As rotas de fallback:
   ```bash
   kubectl get ingressroute -n traefik
   ```

## Atualização

Para atualizar a instalação:

```bash
helm upgrade traefik traefik/traefik -n traefik -f values.yaml
```

Para atualizar a configuração de fallback:

```bash
kubectl apply -f fallback-config.yaml
```

## Desinstalação

Para remover o Traefik:

```bash
helm uninstall traefik -n traefik
kubectl delete namespace traefik
```

Para remover a configuração de fallback:

```bash
kubectl delete -f fallback-config.yaml
```

## Notas Importantes

- O Load Balancer externo é acessível via internet
- O Load Balancer interno só é acessível dentro da VPC
- O roteador de fallback captura todo tráfego não correspondente
- Certifique-se de configurar corretamente os Security Groups
- Monitore os logs para identificar possíveis problemas 