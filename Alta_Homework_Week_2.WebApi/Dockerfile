FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Alta_Homework_Week_2.WebApi/Alta_Homework_Week_2.WebApi.csproj", "Alta_Homework_Week_2.WebApi/"]
RUN dotnet restore "Alta_Homework_Week_2.WebApi/Alta_Homework_Week_2.WebApi.csproj"
COPY . .
WORKDIR "/src/Alta_Homework_Week_2.WebApi"
RUN dotnet build "Alta_Homework_Week_2.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Alta_Homework_Week_2.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alta_Homework_Week_2.WebApi.dll"]
