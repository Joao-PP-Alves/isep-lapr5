import React, { useState, createRef, useEffect } from 'react';
import initializeScene from './initializeScene';
import * as THREE from 'three';

parameters = {
    scene = THREE.Scene(),
    camera = THREE.Camera(),
    renderer = THREE.WebGLRenderer(),
    canvas = canvas.nativeElement,


}

export default class Graph {
    constructor(div,userId){
        const { scene, renderer, camera } = initializeScene(div);
        getData();
        this.initializeScene();

    }


    getData() {

        const [requesterName, setRequesterName] = useState("");
        const [requesterEmail, setRequesterEmail] = useState("");

        const fetchAllUser = async () => {
            const requesterData = axios.get(
                Links.MDR_URL() + "/api/users/")
                .then((response2) => {
                    const requesterObj = response2.data;
                    setRequesterName(requesterObj.name.text);
                    setRequesterEmail(requesterObj.email.emailAddress);
                });

        };
        
        // transform json array to array sample[]
        let users = [];

        for (var i = 0; i < searchedVS.length; i++) {
            var obj = searchedVS[i];
            fetchUser(obj.requester.value);
            users.push([obj.id, requesterName, requesterEmail, obj.description.text]);
        }

        function createData(id, requester, requester_email, description) {
            return { id, requester, requester_email, description };
        }

        const rows = [];

        for (let i = 0; i < searchedVS.length; i += 1) {
            rows.push(createData(...users[i]));
        }

        let root;
        let nodes = [];
        users.forEach(element => {
            if (element.id === this.userRequesterId){
                var node = new Node({
                    color : 0x00ff00,
                    radius : 3,
                    user : element,
                    parent : 'undefined',
                    x : 0.0,
                    y : 0.0
                }, this.scene);
                root = node;
                nodes.push(node);
            }
        });

        let edges = [];
        
        this.addDataToScene(users, rows, nodes, edges);
    }

    
    addDataToScene(users, friendships, nodes, edges){
       
        const slice = (2 * Math.PI) / friendships.length;
        let i=0;
        friendships.forEach(fr => {
            if(fr.id === this.userRequesterId){
                const angle = slice * i;
                var node = new Node({
                    color : 0x0000ff,
                    radius : 3,
                    user : fr.requester,
                    parent : fr.id,
                    x : root.x + this.radius * Math.cos(angle), //calcular em radial
                    y : root.y + this.radius * Math.cos(angle) //calcular em radial
                })
                i++;
            }
        });
        

    }

}