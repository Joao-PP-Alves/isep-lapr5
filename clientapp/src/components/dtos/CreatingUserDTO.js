export class CreatingUserDTO {

    constructor(name, email, tags, password, phoneNUmber, birthDate) {
        this.name = name;
        this.email = email;
        this.tags = tags;
        this.password = password;
        this.phoneNUmber = phoneNUmber;
        this.birthDate = birthDate;
    }


    createTags(input_tags) {
        input_tags.forEach((element) => {
            const user_tag = {
                name: element,
            };
            this.tags.push(user_tag);
        });
        this.tags.shift();
    }

}