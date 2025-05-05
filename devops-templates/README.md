
# Introdução 
Este repositório possui um conjunto de _templates_ que podem ser utilizados para simplificar e agilizar a criação de [pipelines como código](https://docs.microsoft.com/en-us/azure/devops/pipelines/?view=azure-devops) utilizando [yaml](https://yaml.org/) markup.

# Getting Started
Para iniciar sua jornada no uso de pipelines como código, recomendamos a leitura dos materiais de referência abaixo. Os vídeos e tutoriais também são bastante efetivos. 

1.	Getting Started
    - [Get started](https://docs.microsoft.com/en-us/azure/devops/pipelines/get-started/?view=azure-devops)

2.	Documentação Oficial (leitura recomendada)
    - [YAML schema reference](https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema?view=azure-devops&tabs=schema)
    - [Tasks](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/?view=azure-devops)
    - [Stages](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/stages?view=azure-devops&tabs=yaml)
    - [Jobs](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/phases?view=azure-devops&tabs=yaml)
    - [Deployment Jobs](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/deployment-jobs?view=azure-devops)
    - [Taks (Steps)](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/tasks?view=azure-devops&tabs=yaml)
    - [Conditions](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/conditions?view=azure-devops&tabs=yaml)
    - [Expressions](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/expressions?view=azure-devops)
    - [Environments](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/environments?view=azure-devops)

3.	Vídeos
    - [What is Azure DevOps?](https://channel9.msdn.com/Blogs/One-Dev-Minute/What-is-Azure-DevOps--One-Dev-Question)
    - [What is Pipeline as Code?](https://channel9.msdn.com/Blogs/One-Dev-Minute/What-is-Pipeline-as-Code--One-Dev-Question?term=pipeline&lang-en=true)
    - [What is Configuration as Code?](https://channel9.msdn.com/Blogs/One-Dev-Minute/What-is-Configuration-as-Code--One-Dev-Question)
    - [What is Infrastructure as Code?](https://channel9.msdn.com/Blogs/One-Dev-Minute/What-is-Infrastructure-as-Code--One-Dev-Question)
    - [How do you handle security in a DevOps world?](https://channel9.msdn.com/Blogs/One-Dev-Minute/How-do-you-handle-security-in-a-DevOps-world--One-Dev-Question?ocid=player), ou porque temos o Fortify Scan nas pipelines!
    - [Building Security into your DevOps Pipeline](https://channel9.msdn.com/Shows/DevOps-Lab/Building-Security-into-your-DevOps-Pipeline)
    - [How do you ensure quality in a DevOps world?](https://channel9.msdn.com/Blogs/One-Dev-Minute/How-do-you-ensure-quality-in-a-DevOps-world--One-Dev-Question)
    - [Real World Scenario Testing using Azure DevOps and automated UI tests](https://channel9.msdn.com/Shows/DevOps-Lab/Real-World-Scenario-Testing-using-Azure-DevOps-and-automated-UI-tests)
    - Build a CI/CD pipeline for API Management, [Part 1](https://channel9.msdn.com/Shows/Azure-Friday/Build-a-CICD-pipeline-for-API-Management-Part-1?term=pipeline&lang-en=true) & [Part 2](https://channel9.msdn.com/Shows/Azure-Friday/Build-a-CICD-pipeline-for-API-Management-Part-2?term=pipeline&lang-en=true)
    - [What is the difference between Continuous Delivery and Continuous Deployment?](https://channel9.msdn.com/Blogs/One-Dev-Minute/What-is-the-difference-between-Continuous-Delivery-and-Continuous-Deployment--One-Dev-Question)

4.	API references
O Azure DevOps possui uma api REST bastante completa que possibilita a automação de diversas tarefas de maneira simples e prática. A referência desta api pode ser encontrada em [https://docs.microsoft.com/en-us/rest/api/azure/devops/?view=azure-devops-rest-5.1&viewFallbackFrom=azure-devops](https://docs.microsoft.com/en-us/rest/api/azure/devops/?view=azure-devops-rest-5.1&viewFallbackFrom=azure-devops).



# Colaborando
Todos podem contribuir para o repositório de Templates. Algumas premissas, contudo, se aplicam:
1. **A master é sagrada!** Não é pemitido comitar diretamente na master. O uso de _force push_ é particularmente proibido. Para contribuir para o repo, crie seu template, valide com testes de uso e abra um *Pull Request*.
2. Siga a estrutura definida para o repositório:

```
> build: templates utilizados durante o build de artefatos.
    > steps: passos a serem utilizados.
    > jobs: conjuntos de passos, que executarão no pool selecionado.
    > stages: um estágio completo de build.
        > samples: exemplos de uso dos templates em nível de *stage*.
> deploy: templates utilizados para publicação de artefatos produzidos anteriormente.
    > steps: passos a serem utilizados.
    > jobs: conjuntos de passos, que executarão no pool selecionado, publicando no ambiente (environment) informado.
    > stages: um estágio completo de publicação.
        > samples: exemplos de uso dos templates em nível de *stage*.
> multistage: templates de pipelines completas, incluindo CI e CD no mesmo template.
    > samples: exemplos de uso dos templates como pipelines completas.
> tests: templates utilizados para testes unitários, de integração, de carga ou de stress.
    > steps: passos a serem utilizados.
    > jobs: conjuntos de passos, que executarão no pool selecionado.
    > stages: conjuntos de passos, que executarão no pool selecionado, publicando no ambiente (environment) informado.
        > samples: exemplos de uso dos templates em nível de *stage*.
> samples: exemplos de pipelines completas, incluido estágios de *Build*, *Deploy* e, quando aplicável, *Testes*.
> variables: templates de variáveis que podem ser reaproveitadas entre diversas pipelines e gerenciadas em um local único.
```
3. Evite deixar quaisquer condicionais *hardcoded* no seus templates. *Agent Pools*, *Environments*, *File Paths*, entre outros, devem ser transformados em [parâmetros](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/templates?view=azure-devops#passing-parameters).
4. Quando um template não atender completamente seu cenário de build/deploy/teste antes de alterar, verifique com outras squads/tribos se alterações podem ser feitas. Lembre-se que este repositório não é exclusivo. Colaborar com outros times é a melhor maneira de evoluir nossos templates:
    1. No seu repositório, crie uma pipeline completamente funcional que atenda suas necessidades.
    2. Após validar sua pipeline, transforme em variáveis os valores chave tais como *Agent Pools*, *Environments*, *File Paths*, *Argumentos de Execução*, e etc.
    3. Crie uma branch e crie/altere os templates a partir do *stage*, passando pelo *job* e chegando nos *steps*.
    4. Abra um *Pull Request* e solicite que seu código seja revisado por seu time, outras squads e pelo time de DevOps. Quanto mais pessoas colaborarem, melhor será o resultado final.

> IMPORTANTE: O uso dos templates após os testes de desenvolvimento deve ser SEMPRE na master branch. 
> 1. Branches **stale** serão apagadas regularmente sem aviso prévio.
> 2. Após 20 dias da criação de uma branch o autor será notificado para abrir um Pull Request. Branches com 30 ou mais dias serão apagadas regularmente sem novos avisos.

# Exemplos
Exemplos estão disponíveis em nivel de *Build*, *Deploy* e *Tests*. Também existem exemplos de uso de *pipelines* completas incluido as fases de *build* e *deploy*. Quando criar um novo *template* não se esqueça de incluir os exemplos de uso.

# Estrutura
A estrutura de diretórios completa do repositório segue abaixo.
- .vscode : pasta do Visual Studio Code
- build : templates de build
    - jobs : templates de *jobs* de build
    - stages : templates de *stages* de build
        - samples : exemplos de uso dos templates em nível de *stage*
    - steps : templates de *steps* de build
- deploy : templates de deploy
    - jobs : templates de *jobs* de deploy
    - stages : templates de *stages* de deploy
        - samples : exemplos de uso dos templates em nível de *stage*
    - steps : templates de *steps* de deploy
- media : arquivos de mídia do README.md
- multistage : pipelines completas com CI / CD
    - samples : exemplos de uso dos templates de pipelines completas
- samples : exemplos de *pipelines* completas
- tests : templates de testes
    - jobs : templates de *jobs* de testes
    - stages : templates de *stages* de testes
        - samples : exemplos de uso dos templates em nível de *stage*
    - steps : templates de *steps* de testes
- variables : templates de *variáveis* para centralizar gestão de valores

### Saiba mais sobre templates na [documentação oficial](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/templates?view=azure-devops).