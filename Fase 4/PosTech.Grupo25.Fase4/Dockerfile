#imagem do sdk
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

#Copiando csproj em diferentes camadas
COPY *.sln .
COPY PosTech.Entidades/*.csproj ./PosTech.Entidades/
COPY PosTech.Repository/*.csproj ./PosTech.Repository/
COPY PosTech.Contatos.API/*.csproj ./PosTech.Contatos.API/
COPY PosTech.Contatos.API.Tests/*.csproj ./PosTech.Contatos.API.Tests/
COPY PosTech.Contatos.Cadastro.API/*.csproj ./PosTech.Contatos.Cadastro.API/
COPY PosTech.Contatos.Alteracao.API/*.csproj ./PosTech.Contatos.Alteracao.API/
COPY PosTech.Contatos.Exclusao.API/*.csproj ./PosTech.Contatos.Exclusao.API/
COPY PosTech.Cadastro.Consumer/*.csproj ./PosTech.Cadastro.Consumer/
COPY PosTech.Alteracao.Consumer/*.csproj ./PosTech.Alteracao.Consumer/
COPY PosTech.Exclusao.Consumer/*.csproj ./PosTech.Exclusao.Consumer/

#restaurando dependencias
RUN dotnet restore

#copiando restante dos projetos
COPY PosTech.Entidades/. ./PosTech.Entidades/
COPY PosTech.Repository/. ./PosTech.Repository/
COPY PosTech.Contatos.API/. ./PosTech.Contatos.API/
COPY PosTech.Contatos.API.Tests/. ./PosTech.Contatos.API.Tests/
COPY PosTech.Contatos.Cadastro.API/. ./PosTech.Contatos.Cadastro.API/
COPY PosTech.Contatos.Alteracao.API/. ./PosTech.Contatos.Alteracao.API/
COPY PosTech.Contatos.Exclusao.API/. ./PosTech.Contatos.Exclusao.API/
COPY PosTech.Cadastro.Consumer/. ./PosTech.Cadastro.Consumer/
COPY PosTech.Alteracao.Consumer/. ./PosTech.Alteracao.Consumer/
COPY PosTech.Exclusao.Consumer/. ./PosTech.Exclusao.Consumer/

# Definir o nome da API como argumento de build
ARG API_NAME
WORKDIR /App/${API_NAME}

#build e publisj do app
RUN dotnet publish -c Release -o out && ls -la out

#Build da imagem runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Re-declare ARG no estágio final
ARG API_NAME

# Copiar a saída publicada para a imagem final
COPY --from=build-env /App/${API_NAME}/out ./

#Expondo a porta que a aplicação vai rodar
EXPOSE 8080

ENTRYPOINT [ "dotnet", "${API_NAME}.dll" ]
