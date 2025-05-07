# Configuração de Fallback do Traefik

Este documento descreve como configurar o roteamento de fallback no Traefik para direcionar tráfego não correspondente para outro proxy.

## Arquivos

- `fallback-config.yaml`: Contém a configuração do roteamento de fallback

## Configuração

O arquivo `fallback-config.yaml` contém:

1. **Serviço de Fallback**
   - Define o serviço que receberá o tráfego não correspondente
   - Configura o endereço do proxy de fallback

2. **Roteador de Fallback**
   - Captura todo tráfego não correspondente
   - Direciona para o serviço de fallback
   - Aplica middlewares necessários

3. **Middleware**
   - Configura o strip prefix para manipulação de rotas
   - Define os prefixos que serão removidos

## Instalação

```bash
# Aplicar a configuração de fallback
kubectl apply -f fallback-config.yaml
```

## Verificação

```bash
# Verificar o serviço de fallback
kubectl get service -n traefik fallback-proxy

# Verificar o roteador de fallback
kubectl get ingressroute -n traefik fallback-route

# Verificar o middleware
kubectl get middleware -n traefik fallback-strip-prefix
```

## Personalização

Para personalizar a configuração:

1. Edite o arquivo `fallback-config.yaml`
2. Ajuste o endereço do proxy de fallback
3. Modifique os prefixos no middleware conforme necessário
4. Aplique as alterações:
   ```bash
   kubectl apply -f fallback-config.yaml
   ```

## Troubleshooting

### Problemas Comuns

1. **Tráfego não está sendo redirecionado**
   - Verificar se o serviço de fallback está acessível
   - Verificar se os prefixos estão configurados corretamente

2. **Erro 404 no proxy de fallback**
   - Verificar se o endereço do proxy está correto
   - Verificar se o proxy está respondendo

3. **Problemas com prefixos**
   - Verificar se os prefixos no middleware correspondem às rotas
   - Ajustar a ordem dos middlewares se necessário

### Logs

```bash
# Verificar logs do Traefik
kubectl logs -n traefik -l app.kubernetes.io/name=traefik
```

## Remoção

Para remover a configuração de fallback:

```bash
kubectl delete -f fallback-config.yaml
``` 