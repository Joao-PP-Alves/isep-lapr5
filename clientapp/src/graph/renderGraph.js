import React from 'react';
import initializeScene from './initializeScene';
import Node from './node';
import renderToSprite from './renderToSprite';

export default async function renderGraph(div) {
  const { scene, renderer, camera } = initializeScene(div);
  const root = data.find((node) => node.parent === undefined);
  const level1 = data.filter((node) => node.parent === root.id);
  root.x = 0;
  root.y = 0;
  root.level = 0;

  await addMindMapNode(scene, root);
  const radius = 2;
  for (const level1node of level1) {
    level1node.level = 1;
    // TODO:
    //level1node.x = ?;
    //level1node.y = ?;
    await addMindMapNode(scene, level1node);
  }
  renderer.render(scene, camera);
}