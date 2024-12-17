FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 5002
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG RELEASE_STATE=Release
WORKDIR /src
COPY [ "orderform.csproj","orderform/" ]
RUN dotnet restore "./orderform/orderform.csproj"
COPY [".","orderform/"]
WORKDIR "/src/orderform"
RUN dotnet build "orderform.csproj" -c ${RELEASE_STATE} -o /app/build

FROM build AS publish
ARG RELEASE_P_STATE=Release
RUN dotnet publish "./orderform.csproj" -c ${RELEASE_P_STATE} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "orderform.dll" ]