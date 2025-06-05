# Structure des tables pour la démo ADO & EFCore

## Planet
```
- Id        INT
- Name 	    NVARCHAR
- Satelite  INT
- Gravity   DECIMAL
```

## Galaxy
```
- Id          INT
- Name        NVARCHAR
- Description NVARCHAR
```

## Star
```
- Id        INT
- Name      NVARCHAR
- isDeath   BOOLEAN
```

## Rel__Star_Planet
- StarId
- PlanetId

# Relation entre les tables
One to many :
- Star & Galaxy

Many to many :
- Star & Planet