import React, {useState, useEffect} from "react";
import "../../../App.css";
import { isValidEmail, isValidPhone } from "../../../validation";

function CreateUser({show, onClose}) {
    const {register, handleSubmit, errors} = useForm();

    const [allUsersTypes, setAllUserTypes] = useState([]);
    const [userTypes, setAllUserTypes] = useState([]);
    
    useEffect(() => {
        fetchAllUserTypes();
        
    }, []);

    const fetchAllUserTypes = async() => {
        const data = await fetch(config.apiURL + "/usertype");
        const allUserTypes = await data.json();
        setAllUserTypes(
            allUserTypes.map((userTypes) => ({
                label: userType.code,
                value: userType.code,
            }))
        );
    };

    //exemplo para ver se funciona, depois ver melhor isto
    const fetchEmail = async() => {
        const data = await fetch("https://localhost:5001/api/Users")
        const users = await data.json();
        const emailUsers = users.map((d) => {
            return d.email.number;
        });
        setEmails(emailUsers);
    };

    const onSubmit = (data) => {
        console.log(JSON.stringify(data));
        console.log(
            JSOM.stringify({
                ...data,
                types: userTypes.map((dt) => dt.value),
            })
        );
        delete data.userTypes;
        fetch("https://localhost:5000/api/Users", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            // We convert the React state to JSON and send it as the POST body
            body: JSON.stringify({
                ...data,
                types: userTypes.map((dt) => dt.value),
            }),
        })
        .then((response) => {
            console.log(response.status);
            if(!response.ok) {
                const error = data.message || response.status;
                console.log(
                    JSON.stringify({
                        ...data,
                        types: userTypes.map((dt) => dt.value),
                    })
                );
                return error.toString();
            }
            return response.json();
        })
        .then((data) => {
            console.log(data);
            window.location.reload();
        });
    };

    return(
        <>
        <Modal
          size="lg"
          show={show}
          onHide={onClose}
          backdrop="static"
          keyboard={false}
        >
          <form onSubmit={handleSubmit(onSubmit)} className="form">
            <Modal.Header closeButton>
              <Modal.Title>Create New User</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              <div className="divCreate">
                <div>
                  <h3>Email</h3>
                  <label className="createlabel">Name: </label>
                  <input
                    className="nameInput"
                    type="text"
                    name="name"
                    ref={register({ required: true })}
                    placeholder="Name of the new user"
                  />
                  {errors.name && "Name is required."}
                </div>
                <div>
                  <label className="createlabel">Email: </label>
                  <input
                    className="emailInput"
                    type="text"
                    name="email"
                    ref={register({
                      required: true,
                      validate: isValidEmail(value),  //validates the email
                    })}
                    placeholder="Email of the new User"
                  />
                  {errors.email &&
                    "The email must contain a '@' and a '.'. (eg.: user@email.com)"}
                </div>
                <div>
                  <label className="createlabel">Phone Number: </label>
                  <input
                    className="phoneNumberInput"
                    type="text"
                    name="phone number"
                    maxlength="9"
                    ref={register({
                       required: true, 
                       validate: isValidPhone(value), //validates phone number
                    })}
                    placeholder="Phone number of the new user"
                  ></input>
                  {errors.phoneNumber && "Phone number must be a 9 digits number."}
                </div>
                <div>
                  <label className="createlabel">Birthday Date: </label>
                  <input
                    className="birthdayInput"
                    type="date"
                    name="birthday"
                    ref={register({ required: true })}
                    placeholder="Birthday of the new user"
                  />
                  {errors.birthday && "Birthday is required."}
                </div>
                <hr />
                <div>
                  <h3>License</h3>
                  <label className="createlabel">Emotional State: </label>
                  <input
                    className="emotionalStateInput"
                    type="text"
                    name="emotional state"
                    ref={register({ required: true })}
                    placeholder="Emotional State of the new User"
                  />
                  {errors.license && "Emotional state is required."}
                </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
              <input className="userSubmitButton" type="submit" />
            </Modal.Footer>
          </form>
        </Modal>
      </>
    );
  }
  export default CreateUser;
  