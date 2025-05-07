# Traefik no EKS

Este diretório contém os manifestos Kubernetes para instalar e configurar o Traefik como ingress controller no EKS, utilizando AWS Load Balancer Controller.

## Estrutura de Arquivos

- `00-ingress-class.yaml`: Define o IngressClass para o AWS Load Balancer Controller
- `01-namespace.yaml`: Cria o namespace dedicado para o Traefik
- `01-rbac.yaml`: Configura as permissões do Traefik
- `02-deployment.yaml`: Configura o deployment do Traefik
- `03-service-external.yaml`: Configura o serviço externo com ALB
- `04-service-internal.yaml`: Configura o serviço interno como ClusterIP
- `05-dashboard-ingress.yaml`: Configura o acesso interno à dashboard do Traefik

## Pré-requisitos

1. Cluster EKS configurado
2. AWS Load Balancer Controller instalado
3. Certificado SSL válido no ACM
4. Subnets configuradas com as tags corretas do Kubernetes

## Instalação

Aplique os manifestos na seguinte ordem:

```bash
# Criar o IngressClass
kubectl apply -f 00-ingress-class.yaml

# Criar o namespace
kubectl apply -f 01-namespace.yaml

# Configurar RBAC
kubectl apply -f 01-rbac.yaml

# Deployar o Traefik
kubectl apply -f 02-deployment.yaml

# Criar os serviços
kubectl apply -f 03-service-external.yaml
kubectl apply -f 04-service-internal.yaml

# Configurar o acesso à dashboard
kubectl apply -f 05-dashboard-ingress.yaml
```

## Verificação da Instalação

```bash
# Verificar o status dos pods
kubectl get pods -n traefik

# Verificar os serviços
kubectl get svc -n traefik

# Verificar os ingress
kubectl get ingress -n traefik

# Verificar os logs
kubectl logs -n traefik -l app.kubernetes.io/name=traefik
```

## Acesso aos Serviços

### Dashboard do Traefik (Acesso Interno)
O dashboard é acessível apenas dentro do cluster através do Traefik Ingress:
```bash
# Obter o endereço do Traefik Ingress
kubectl get ingress -n traefik traefik-dashboard -o jsonpath='{.status.loadBalancer.ingress[0].hostname}'
```
Acesse: `http://<TRAEFIK-INGRESS-IP>/dashboard/`

### Serviço Externo
```bash
# Obter o DNS do ALB externo
kubectl get svc -n traefik traefik -o jsonpath='{.status.loadBalancer.ingress[0].hostname}'
```
Acesse: `https://<DNS-DO-ALB-EXTERNO>/`

### Serviço Interno
O serviço interno é acessível apenas dentro do cluster através do ClusterIP:
```bash
# Obter o ClusterIP
kubectl get svc -n traefik traefik-internal -o jsonpath='{.spec.clusterIP}'
```
Acesse: `http://<CLUSTER-IP>:8080/`

## Configuração

### Portas
- Externo:
  - HTTP: 80
  - HTTPS: 443
- Interno (ClusterIP):
  - HTTP: 8080
  - HTTPS: 8443

### Recursos
- CPU Request: 100m
- CPU Limit: 500m
- Memory Request: 128Mi
- Memory Limit: 512Mi

### Replicas
- 2 réplicas para alta disponibilidade

## Troubleshooting

### Verificar Logs
```bash
# Logs do Traefik
kubectl logs -n traefik -l app.kubernetes.io/name=traefik

# Descrever pods
kubectl describe pods -n traefik -l app.kubernetes.io/name=traefik
```

### Problemas Comuns
1. **ALB não é criado**
   - Verificar se o AWS Load Balancer Controller está instalado
   - Verificar se as subnets têm as tags corretas

2. **Dashboard não acessível**
   - Verificar se o Traefik está configurado como Ingress Controller
   - Verificar se o serviço interno está respondendo na porta 8080
   - Verificar se o path `/dashboard` está configurado corretamente

3. **Serviços internos não acessíveis**
   - Verificar se o ClusterIP está configurado corretamente
   - Verificar se os pods estão rodando
   - Verificar se as portas estão corretas

4. **Erros de permissão**
   - Verificar se o RBAC está configurado corretamente
   - Verificar se o ServiceAccount está sendo usado
   - Verificar se o ClusterRoleBinding está correto

## Manutenção

### Atualização
Para atualizar o Traefik, basta aplicar novamente os manifestos:
```bash
kubectl apply -f .
```

### Remoção
Para remover todos os recursos:
```bash
kubectl delete -f .
``` 