export interface Contract{
    id: number;
    name: string;
    unit: string;
    unitId: number;
    iban: string;
    tennant: string;
    tennantId: number;
    price: number;
    rentFrom: Date;
    rentTo: Date;
    displayString: string;
}