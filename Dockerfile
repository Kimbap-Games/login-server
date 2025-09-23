# 1. 빌드 환경: .NET 9 SDK 이미지를 기반으로 시작합니다.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 2. 종속성 복원: .csproj 파일을 먼저 복사하여 NuGet 패키지를 복원합니다.
#    - 소스 코드가 변경되어도 패키지는 다시 다운로드하지 않도록 캐싱 효율을 높입니다.
COPY ["LoginServer/LoginServer.csproj", "LoginServer/"]
RUN dotnet restore "LoginServer/LoginServer.csproj"

# 3. 소스 코드 복사: 나머지 모든 소스 코드를 복사합니다.
COPY . .

# 4. 애플리케이션 빌드 및 게시(Publish): 릴리스 모드로 최적화된 결과물을 만듭니다.
WORKDIR "/src/LoginServer"
RUN dotnet publish "LoginServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 5. 최종 이미지: 가벼운 ASP.NET 9 런타임 이미지를 기반으로 최종 이미지를 만듭니다.
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# 6. 게시된 결과물 복사: 빌드 단계에서 생성된 결과물만 최종 이미지로 가져옵니다.
COPY --from=build /app/publish .

# 7. 컨테이너 시작 명령어: 컨테이너가 시작될 때 애플리케이션을 실행합니다.
ENTRYPOINT ["dotnet", "LoginServer.dll"]