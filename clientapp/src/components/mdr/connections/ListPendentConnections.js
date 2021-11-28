import React, { useState, useEffect} from 'react';
import './Table.css';
import Links from "../../Links";
import axios from 'axios';

export default function ListPendentConnections() {

    useEffect(() => {
        search();
    }, []);

    const [searchedVS, setSearchedVS] = useState([]);

    const fetchPC = async () => {
        
        const data = await fetch(
            Links.MDR_URL()+"/api/connections/pendent/f4b32456-5989-4e2a-b8fd-49b2197ebfea"
        );
        const vsList = await data.json();
        console.log(vsList);
        setSearchedVS(vsList);

    };
    function search() {
        fetchPC();

    }

    function fillWorkblockCode(list) {
        var wbCode = "";
        if (list.length !== 0) {
            list.map((elem) => (
                wbCode = wbCode + elem.workblockCode.code + "; "
            ))
        }
        return wbCode
    }

    return (
        <div className="divCreateLineUI" data-testid="lineTest">
            <h1>List Pendent Connections</h1>
            <br />
            <table className="table">
                <tr>
                    <th>ID</th>
                    <th>Requester</th>
                    <th>Description</th>
                </tr>
                {searchedVS.map((vs) => (
                    <tr>
                        <td>{vs.id}</td>
                        <td>{vs.requester.value}</td>
                        <td>{vs.description.text}</td>
                    </tr>
                ))}
            </table>
        </div>
    );
}