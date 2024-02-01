export class Register {
    id:number;
    name: string;
    surname : string;
    email: string;

    constructor(id: number, name: string, email: string,surname : string) {
        this.id = id;
        this.name = name;
        this.email = email;
        this.surname = surname;
      }
}

