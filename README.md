LdgLite

Genera classifiche per il torneo Luca De Gerone utilizzando un db sqlite.
I dati in ingresso sono in CSV (sep. da `;`) e utilizza (FileHelper)[http://www.filehelpers.com/] per
eseguire il parser.

# Creazione del db

```
$ sqlite3 ./db/ldg2011.sqlite < ./sql/create.sql
```

# Opzioni
## action
Azione da eseguire. Puo' essere:
* Interactive
* Insert
* Create
* CreateResult
* Module


## year
Anno dell'edizione. Usato solo da _create_

## database
Nome del database. Usato da tutti

## input
Nome del file di input in csv. Usato solo da _insert_

## config
Nome del file di configurazione. E' usato da _createlist_ e _createresult_ e
assume significati diversi. 

Con _createlist_ indica il file con le queries per
ottenere l'ordine di partenza (non si distingue M/F) per _createresult_ le
query per creare la classifica.

# outout
Nome del file _pdf_ di output. Il default e' `../classifiche/list{anno}.pdf`
usando _createlist_ e `../classifiche/class{anno}.pdf` usando usando
_createresult_ 

# Sinopsi

| action       | year | db | input | config | output |
|--------------+------+----+-------+--------+--------|
| Interactive  | x    | x  | -     | x      | -      |
| Insert       | -    | x  | x     | -      | -      |
| CreateList   | x    | x  | -     | x      | x      |
| CreateResult | x    | x  | -     | x      | x      |
| Module       | x    | x  |       |        |        |

# Uso

## Creare il modulo di iscrizione

```
Lgd.exe -a module -y2011 -d ../db/ldg2011.sqlite
```

Il db non e' usato, ma si deve passarlo lo stesso. Il report generato e' module.pdf

## Popolare in db in base al file csv di input
```
Lgd.exe -a insert -d ../db/ldg2011.sqlite -i ../iscrizioni/athlete2011.csv
```

Il file di input deve essere specificato. I campoi devono essere separati da
virgola e senza apici.


## Create la lista di partenza
La lista di partenza e' l'elenco degli atleti suddivisi per categoria
 
```
Lgd.exe -a createlist -y2011 -d ../db/ldg11.sqlite
```

Se non indicato il file di configurazione e' `../support/list.csv` e come
file di output `../../classifiche/list2011.pdf`

Per indicare diversamente
```
Lgd.exe -a createlist -y2011 -d ../../db/ldg11.sqlite -i ../../doc/nuovalista.csv -o ../../nuovoreport.pdf
```

## Inserire iterattivamente i tempi  
```
Lgd.exe -a interactive -y2011 -d ../../db/ldg11.sqlite
```

Visualizza gli atleti secondo l'ordine della lista ~../../doc/list.csv~. 
I tempi possono essere inseriti separati con + oppure . ad esempio

| 2+5+36 | 02:05.36   |
|  +5+36 | 00:05.36   |
|     +5 | 00:05.00   |
|    2+5 | 02:05.00   |
|    2+0 | 02:00.00   |


## Create la classifica

``` 
Lgd.exe -a createresult -y2011 -d ../db/ldg2011.sqlite
```

in questo caso come file di configurazione usa `../support/cat.csv` e come
file di output `../classifiche/class2011.pdf`

Per indicare diversamente:

```
Lgd.exe -a createresult -y2011 -d ../db/ldg11.sqlite -i ../support/nuovacat.csv -o ../nuovoreport.pdf
```

# Categorie
Ci sono 4 gruppi di eta' U08 con eta' <= 7, U10 <= 9, U12 <= 11 e U14 <=
13 (I quattordicenni gareggiano con gli adulti)

| Cat. | da |  a |
| ---- | -- | -- |
| U08  |  4 |  7 |
| U10  |  8 |  9 |
| U12  | 10 | 11 |
| U14  | 12 | 13 |

Le categorie sono descritte dal file `support\cat.csv` nella forma
```
sigla, desc, query per ricavarla
```

ad esempio:

```
U08M,Categoria U8 Maschile,select * from at where gender="M"  and ({0} - year) <= 7  order by time
U12F,Categoria U12 Femminile, select * from at where gender="F" and ({0} - year) >= 10 and ({0} - year) <= 11  order by time
```
