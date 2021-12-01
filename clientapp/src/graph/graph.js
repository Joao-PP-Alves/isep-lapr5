import React, { useState, createRef, useEffect } from 'react';
import * as THREE from 'three';

parameters = {
    scene = THREE.Scene(),
    camera = THREE.Camera(),
    renderer = THREE.WebGLRenderer(),
    canvas = canvas.nativeElement,


}

export default class Graph {
    constructor(userId){
        this.userRequesterId = userId;
        this.scene = new THREE.Scene();
        this.camera = new THREE.PerspectiveCamera(
            75,
            window.innerWidth / window.innerHeight,
            0.1,
            1000
        );
        this.renderer = new THREE.WebGLRenderer();
        renderer.setSize(window.innerWidth, window.innerHeight);
        camera.position.z = 5;
        
        addDatatoScene();
        this.initializeScene();

    }

    addDatatoScene() {

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

        let nodes = [];
        users.forEach(element => {
            if (element.id === this.userRequesterId){
                var node = new Node({
                    color : 0x00ff00,
                    user : element,
                    parent : 'undefined',
                    x : 0.0,
                    y : 0.0
                }, this.scene);
                nodes.push(node);
            }
        });

        //Need friendships Here
        let friendships = [];

        //
        /** let edges = [];
        friendships.forEach(fr => {
            if(fr."friend1".id === this.userRequesterId){
                var node = new Node({
                    color : 0x0000ff,
                    user : fr.friend2,
                    parent : fr.friend1,
                    x : 0.1, //calcular em radial
                    y : 0.1 //calcular em radial
                })
            }
        })*/

    }

    initializeScene(scene,camera,renderer,graph) {
        scene.add(graph);
        renderer.renderer(scene,camera);
        return;
    } 
    
}