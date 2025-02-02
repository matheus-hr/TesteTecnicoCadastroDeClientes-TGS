FROM mcr.microsoft.com/mssql/server:2019-latest

# Garantir que o container use o usuário root para a instalação
USER root

# Definir o ambiente para o SQL Server
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Developer

# Expor a porta padrão do SQL Server
EXPOSE 1433

# Comando para rodar o SQL Server
CMD /opt/mssql/bin/sqlservr
