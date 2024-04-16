FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MSAuth/MSAuth.API.csproj", "MSAuth/"]
COPY ["MSAuth.Application/MSAuth.Application.csproj", "MSAuth/"]
COPY ["MSAuth.Domain/MSAuth.Domain.csproj", "MSAuth/"]
COPY ["MSAuth.Infrastructure/MSAuth.Infrastructure.csproj", "MSAuth/"]
RUN dotnet restore "MSAuth/MSAuth.API.csproj"
COPY . .
WORKDIR "/src/MSAuth"
RUN dotnet build "MSAuth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MSAuth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish 


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "MSAuth.API.dll" ]