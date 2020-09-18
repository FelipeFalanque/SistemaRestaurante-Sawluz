# SistemaRestaurante-Sawluz
* teste realizado na empresa sawluz
* garanto que nos nos dias de hoje entregaria uma solução bem melhor do que essa.

Para Funcionar Corretamente Todos os asquivos do sitema devem estar nesse diretorio
Como mosta a imagem Imagem01
C:\SistemaRestaurantes\SistemaRestaurantes\

-------------------------------------------------------------------------------------

Caso não rode o projeto em C: é preciso mudar o caminho do banco de dados

Configuração Atual => C:\SistemaRestaurantes\SistemaRestaurantes\BD_Sistema_Restaurante.mdf

WebSite/Web.config
TesteRepositorio/app.config
RepositorioDados/app.config

<connectionStrings>
	<add name="BD_Sistema_RestauranteConnectionString"
	connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
	C:\SistemaRestaurantes\SistemaRestaurantes\BD_Sistema_Restaurante.mdf;Integrated Security=True;
	Connect Timeout=30"
	providerName="System.Data.SqlClient" />
</connectionStrings>
