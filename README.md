# SEappIII

### Running

Open two terminals.
In one terminal do:
```
cd roommate-app
dotnet run
```
In another terminal do:
```
cd react-client
npm start
```
Plan by month:
1. implement posting a listing (including preferences like budget, how many people, description etc.) 
2. implement responding to a listing (in-app communication like messaging) 
3. implement attaching people to a listing (accepting people to be your roommate) 
4. implement closing a listing *. at some point implement user registration system *. when required implement communication with database

Plan (Dominykas, Edvinas, Karolis, Domantas)
1 menesis - implement posting a listing:

1sav:
- Create a listing button that opens a new window
- Creates new window - user input
- Creates button which saves the user to the file
- Display the saved info on the first page

2sav:
- Create an account
- Input validation and formik
- Sign in into an account
- css listing + design

3sav:
- fix the mistakes from last week
- fix listing submission
- enum (and possibly IEnumerable) for cities and dropdown for cities in frontend
- fix login with react-router

4sav:
- IComparer for sorting, Filtering listings by city, price, number of roommates, 10
- validacija turi būti ir backend'e (tarkim jeigu siųstų requestą tiesiai į API), 4, 7 ?
- enumą reikia naudot kaip enumą, 3, 8?
- fix issues

5sav:
NOTE VISIEM: pabandyk widening/narrowing conversions kur nors įkišt ir named parameters (nėra sunku)

- dominykas
	+ pasiimant sorted listings naudot query parametrus vietoj POST
	+ negrąžina listingų, reik taisyt (priminimas, reikia pakeist json failą)
	+ reikia roommateCount keisti į int'ą
	+ padaryt kad location veiktų
- edvinas
	+ string.IsNullOrEmpty -> string.IsNullOrWhitespace pakeist
	+ validacijoj reikia gražinti tiesiog expression, o ne false
	+ įvykdyt kažkurį vieną punktą iš antro etapo
- karolis
	+ datą įstatyt į listing'ą reikia backend'e, o ne front'e
	+ datai yra datetime tipas, nenaudot string
	+ dependency injection 2.9
- domantas
	+ login/router
