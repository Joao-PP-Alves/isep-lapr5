//regex to validate the strings

const EMAIL = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const PASSWORD = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.* )(?=.*[^a-zA-Z0-9]).{8,}$/;
const NAME = /^[A-Za-z\s]+$/;

module.exports = {
  isValidUsername: username => !(username.length < 3 || username.length > 15),
  isValidEmail: email => EMAIL.test(email.toLowerCase()),
  isValidPassword: password => PASSWORD.test(password),
  isValidName: name => NAME.test(name),
  isValidPhone: phone => phone.length >= 6,
  isValidMessage: message => message.length >= 0,
  curateTitle: title => title.trim(),
  curateDescription: description => description.trim(),
  curateAddress: address => address.trim(),
  curatePhoneNumber: phoneNumber => phoneNumber.trim(),
};
