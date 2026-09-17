# Étape 1 : build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AiMicroservice.Api/AiMicroservice.Api.csproj", "AiMicroservice.Api/"]
COPY ["AiMicroservice.Application/AiMicroservice.Application.csproj", "AiMicroservice.Application/"]
COPY ["AiMicroservice.Domain/AiMicroservice.Domain.csproj", "AiMicroservice.Domain/"]
COPY ["AiMicroservice.Infrastructure/AiMicroservice.Infrastructure.csproj", "AiMicroservice.Infrastructure/"]

RUN dotnet restore "AiMicroservice.Api/AiMicroservice.Api.csproj"

COPY . .
WORKDIR "/src/AiMicroservice.Api"
RUN dotnet publish "AiMicroservice.Api.csproj" -c Release -o /app/publish

# Étape 2 : image finale (plus légère, sans le SDK)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "AiMicroservice.Api.dll"]