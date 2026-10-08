# Support Desk

## Syfte

Support Desk är en applikation för att hantera supportärenden. Användaren kan registrera kunder, skapa supportärenden, uppdatera ärenden, tilldela handläggare, ändra status och prioritet samt lägga till kommentarer.

Applikationen är byggd som ett grupprojekt i C# med WinUI, Clean Architecture, DDD, Repository Pattern, Service Pattern och dependency injection.

## Gruppmedlemmar

- Ali
- Sebastian
- Scott
- Malin

## Bygga och starta applikationen

1. Klona repositoryt från GitHub.
2. Öppna solutionen `SupportDesk.slnx` i Visual Studio.
3. Kontrollera att alla NuGet-paket och projektberoenden har laddats.
4. Bygg solutionen.
5. Starta `SupportDesk.Presentation`.

## Köra tester

Testerna finns i projektet:

`SupportDesk.Tests`

Testerna kan köras från Test Explorer i Visual Studio.

Projektet innehåller både enhetstester och integrationstester.

## Projektstruktur

Lösningen är uppdelad i fyra huvudprojekt.

### SupportDesk.Domain

Innehåller verksamhetens modeller och regler.

Exempel:

- `Customer`
- `SupportTicket`
- `TicketComment`
- `TicketStatus`
- `TicketPriority`

Domänmodellerna ansvarar för regler som exempelvis validering, statusändringar, tilldelning av handläggare och kommentarer.

### SupportDesk.Application

Innehåller applikationens services och kontrakt.

Exempel:

- `TicketService`
- `CustomerService`
- service-interface
- repository-interface

Application samordnar arbetet mellan användargränssnittet, domänmodellerna och repositories.

### SupportDesk.Infrastructure

Ansvarar för lagring och inläsning av data.

Här finns JSON-repositories som läser och skriver kunder och supportärenden till JSON-filer.

### SupportDesk.Presentation

Innehåller WinUI-applikationens användargränssnitt.

Här finns bland annat:

- Pages
- ViewModels
- navigering
- XAML
- dependency injection-konfiguration

Presentation använder services från Application för att utföra funktioner.

## Hur projekten samverkar

Ett vanligt flöde i applikationen är:

Presentation  
→ Application  
→ Domain  
→ Repository  
→ Infrastructure  
→ JSON-fil

Presentation hanterar användarens inmatning.

Application samordnar vad som ska göras.

Domain innehåller verksamhetsreglerna.

Infrastructure ansvarar för att spara och läsa data.

## JSON-lagring

Applikationen använder lokala JSON-filer för att spara information.

Ticket-data sparas i:

`%LocalAppData%\SupportDesk\tickets.json`

Kunddata sparas också lokalt i SupportDesk-mappen.

Exakt filstruktur kan uppdateras när all lagringsfunktionalitet är färdig.

Applikationen ska kunna starta även om JSON-filerna ännu inte finns. Om en befintlig JSON-fil innehåller felaktig data ska felet hanteras utan att den befintliga informationen automatiskt skrivs över.

## Arbetsfördelning

### Ali – Ärendehantering

Ansvarar främst för:

- visa ärendedetaljer
- uppdatera rubrik och beskrivning
- tilldela handläggare
- ändra status
- ändra prioritet
- lägga till kommentarer
- tester för ärendehantering

### Sebastian – Överblick, sökning och filtrering

Ansvarar främst för:

- lista ärenden
- sökning
- filtrering efter status
- översikt
- statistik för ärendestatus
- tester för sökning och filtrering

### Scott – Kundhantering

Ansvarar främst för:

- registrera kunder
- uppdatera kunder
- kundvalidering
- kundservice
- lagring av kunder
- tester för kundhantering

### Malin – Ärenderegistrering

Ansvarar främst för:

- skapa nya supportärenden
- välja kund
- välja prioritet
- formulär för nya ärenden
- tester för ärenderegistrering

Alla gruppmedlemmar deltar även i kodgranskning, integration, tester och gemensamma arkitekturbeslut.

## Kravkontroll

- [ ] Registrera kund
- [ ] Visa kunder
- [ ] Uppdatera kund
- [ ] Registrera supportärende
- [ ] Koppla ärende till kund
- [ ] Visa ärendelista
- [ ] Visa ärendedetaljer
- [ ] Ändra rubrik och beskrivning
- [ ] Tilldela handläggare
- [ ] Ändra prioritet
- [ ] Ändra status
- [ ] Lägga till kommentarer
- [ ] Söka efter ärenden
- [ ] Filtrera ärenden efter status
- [ ] Visa antal ärenden per status
- [ ] Navigering mellan sidor
- [ ] Spara data till JSON
- [ ] Läsa tillbaka data från JSON
- [ ] Bevara id och relation mellan kund och ärende
- [ ] Hantera felaktig JSON
- [ ] Minst sex enhetstester
- [ ] Minst två integrationstester
- [ ] Manuell kontroll genomförd

Checklistan uppdateras när funktionerna färdigställs.

## Tester

### Enhetstester

Projektet ska bland annat kontrollera:

- att ett giltigt ärende skapas med status Nytt
- att tom rubrik avvisas
- att ett ärende utan handläggare inte kan ändras till Pågående
- att status kan ändras när handläggare finns
- att sökning fungerar oberoende av stora och små bokstäver
- att statusfiltrering fungerar

### Integrationstester

Integrationstesterna ska använda den riktiga JSON-lagringen.

Tester ska bland annat kontrollera:

- att kund och ärende kan sparas och läsas tillbaka
- att id-värden och kundkoppling finns kvar
- att handläggare, status och kommentarer finns kvar efter omladdning
- att felaktig JSON hanteras utan att filens innehåll skrivs över med tom data

## Manuell kontroll

Följande arbetsflöde ska kontrolleras manuellt innan inlämning:

- [ ] Registrera en kund
- [ ] Skapa ett nytt supportärende
- [ ] Öppna ärendedetaljer
- [ ] Tilldela en handläggare
- [ ] Ändra status
- [ ] Ändra prioritet
- [ ] Lägg till en kommentar
- [ ] Söka efter ärendet
- [ ] Filtrera ärenden
- [ ] Starta om applikationen
- [ ] Kontrollera att sparad information finns kvar

Resultatet av den manuella kontrollen fylls i när applikationen är färdig.

## Kända begränsningar

Den här sektionen uppdateras när projektet närmar sig färdigställande.

Applikationen använder lokal JSON-lagring och är avsedd för en användare och en körande instans åt gången.

## AI-användning

AI-verktyg har använts som stöd för bland annat:

- förklaringar av programmeringskoncept
- felsökning
- diskussion av arkitektur
- kodförslag
- code review

Förslag från AI har granskats innan de har lagts in i projektet. Gruppen ansvarar själva för den kod som används och kontrollerar ändringar genom kodgranskning, byggning och tester.