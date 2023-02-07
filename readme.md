## Proiect .NET - Sandu Eduard Alexandru Grupa 234

Acest proiect constituie simularea unui magazin online (doar backend)

## Unde am rezolvat cerintele:
 
#### Backend:

 -3 Controllere (minim); Fiecare Metoda Crud, REST cu date din baza de date. (1p)  -> **Folderul Controllers**
 -Cel puțin 1 relație între tabele din fiecare fel (One to One, Many to Many, One to Many); -> **Folderul Data -> AppDbContext**
 Folosirea metodelor din Linq: GroupBy, Where, etc; Folosirea Join si Include (1p) -> **Repositories -> Orders, Products**
 -Autentificare + Roluri; Autorizare pe endpointuri în funcție de Roluri; Cel putin 2 Roluri: Admin, User (1p) -> **Controllers -> AuthController**
 -Sa se foloseasca repository pattern + Service (1p) **bifat*
 
 #### Puncte Extra:
 
 -Unit of Work (1p) -> **Folderul Repositories -> Folderul Unit Of Work**
 -Refresh Token (1p) -> **In AuthController au fost apelate metodele la login/refresh, si in Helpers/JwtUtils a fost generat** 
