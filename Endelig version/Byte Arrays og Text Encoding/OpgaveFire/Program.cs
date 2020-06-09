// Basalt set består ASCII af de første 127 tegn I UTF-8, altså de tegn der kan
// udtrykkes ved hjælp af de første syv tegn i en byte (den ottende benyttes til at
// fortælle hvorvidt vi er i den første, anden, tredje eller fjerde byte).

// Hvis man inputter en string der kun består af ASCII tegn vil decoderen derfor kun forholde sig til den første
// byte af de fire UTF-8 består af.

// Indsætter man tegn der ligger udover dette bliver det signaleret af den ottende bit. Herefter vil decoderen kigge på
// den næste byte og så fremdeles indtil den finder en byte hvor ottende byte signalerer at det er denne byte hvorfra
// kodningen af tegnet starter. Herefter vil decoderen bruge denne og de tidligere bytes til at "slå op" i UTF8 og
// finde det præcise tegn der er tale om

