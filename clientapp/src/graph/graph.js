import React, { createRef, useEffect } from 'react';
import * as THREE from 'three';

export default function Graph() {
    const divRef = createRef();
    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(
        75,
        window.innerWidth / window.innerHeight,
        0.1,
        1000
    );
    const renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);
    divRef.current.appendChild(renderer.domElement);

    camera.position.z = 5;

    //alterar isto para dados do react

    const [requesterName, setRequesterName] = useState("");
    const [requesterEmail, setRequesterEmail] = useState("");

    const fetchUser = async (requesterId) => {
        const requesterData = axios.get(
            Links.MDR_URL() + "/api/users/" + requesterId
        ).then((response2) => {
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

    // push the information from sample to rows

    const rows = [];

    for (let i = 0; i < searchedVS.length; i += 1) {
        rows.push(createData(...users[i]));
    }

    //const root = data.find(());

    initializeScene(scene, camera, renderer, this);



}

export default function initializeScene(scene, camera, renderer, graph) {
    scene.add(graph);
    renderer.renderer(scene, camera);
    return;
}