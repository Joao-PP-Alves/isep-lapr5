import React from 'react';
import initializeScene from './initializeScene';
import renderToSprite from './renderToSprite';
import Node from './node'
import data from './info'

export default async function renderGraph(div) {
  const { scene, renderer, camera } = initializeScene(div);
  const root = data.find((node) => node.parent === undefined);
  const level1 = data.filter((node) => node.parent === root.id);
  root.x = 0;
  root.y = 0;
  root.level = 0;

  await Node(scene, root);
  const radius = 2;
  const slice = (2 * Math.PI) / level1.length;
  for (let i = 0; i < level1.length; i++) {
    const level1node = level1[i];
    level1node.level = 1;
    const angle = slice * i;
    level1node.x = root.x + radius * Math.cos(angle);
    level1node.y = root.y + radius * Math.sin(angle);
    await Node(scene, level1node);
  }
  renderer.render(scene, camera);
}