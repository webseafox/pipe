# Traefik Ingress Controller

Este documento descreve a configuração do Traefik como Ingress Controller no EKS.

## Arquitetura

O Traefik está configurado com:

1. **Dois Load Balancers**:
   - Externo (internet-facing) com SSL
   - Interno (VPC-only) para tráfego interno

2. **Dashboard**:
   - Acessível via HTTPS
   - Protegido por certificado SSL
   - Path: `/dashboard`

3. **Recursos**:
   - 2 réplicas para alta disponibilidade
   - Recursos limitados para CPU e memória

## Configuração

### Portas

- **Externo**:
  - HTTP: 80
  - HTTPS: 443

- **Interno**:
  - HTTP: 8080
  - HTTPS: 8443

### Recursos

```yaml
resources:
  requests:
    cpu: 100m
    memory: 128Mi
  limits:
    cpu: 500m
    memory: 512Mi
```

### TLS

- Certificado SSL do ACM
- Redirecionamento HTTP para HTTPS
- Suporte a múltiplos domínios

## Instalação

1. **Criar Namespace**:
   ```bash
   kubectl apply -f 01-namespace.yaml
   ```

2. **Deployar Traefik**:
   ```bash
   kubectl apply -f 02-deployment.yaml
   ```

3. **Criar Serviços**:
   ```bash
   kubectl apply -f 03-service-external.yaml
   kubectl apply -f 04-service-internal.yaml
   ```

4. **Configurar Dashboard**:
   ```bash
   kubectl apply -f 05-dashboard-ingress.yaml
   ```

## Acesso

### Dashboard

```bash
# Obter DNS do ALB
kubectl get ingress -n traefik traefik-dashboard -o jsonpath='{.status.loadBalancer.ingress[0].hostname}'
```

Acesse: `https://<DNS-DO-ALB>/dashboard/`

### Serviços

- **Externo**: `https://<DNS-DO-ALB-EXTERNO>/`
- **Interno**: `http://<DNS-DO-ALB-INTERNO>:8080/`

## Monitoramento

### Logs

```bash
# Logs do Traefik
kubectl logs -n traefik -l app.kubernetes.io/name=traefik

# Logs específicos
kubectl logs -n traefik -l app.kubernetes.io/name=traefik -c traefik
```

### Métricas

- Prometheus metrics disponíveis
- Endpoint: `/metrics`

## Troubleshooting

### Problemas Comuns

1. **ALB não é criado**
   - Verificar AWS Load Balancer Controller
   - Verificar tags das subnets
   - Verificar IAM permissions

2. **Dashboard não acessível**
   - Verificar certificado SSL
   - Verificar configuração do ingress
   - Verificar logs do Traefik

3. **Serviços não respondem**
   - Verificar health checks
   - Verificar configuração de portas
   - Verificar security groups

### Comandos Úteis

```bash
# Verificar status dos pods
kubectl get pods -n traefik

# Verificar serviços
kubectl get svc -n traefik

# Verificar ingress
kubectl get ingress -n traefik

# Descrever pods
kubectl describe pods -n traefik -l app.kubernetes.io/name=traefik
```

## Manutenção

### Atualização

```bash
# Atualizar deployment
kubectl apply -f 02-deployment.yaml

# Verificar rollout
kubectl rollout status deployment/traefik -n traefik
```

### Backup

- Configurações salvas em YAML
- Certificados gerenciados pelo ACM
- Estado do cluster gerenciado pelo EKS

## Segurança

### Boas Práticas

1. **Certificados**:
   - Usar certificados do ACM
   - Renovar automaticamente
   - Validar domínios

2. **Acesso**:
   - Limitar acesso à dashboard
   - Usar HTTPS sempre
   - Configurar security groups

3. **Recursos**:
   - Limitar CPU e memória
   - Monitorar uso
   - Configurar autoscaling

## Suporte

Para problemas ou dúvidas:

1. Verificar logs do Traefik
2. Consultar documentação oficial
3. Verificar status do EKS
4. Verificar configuração do ALB 