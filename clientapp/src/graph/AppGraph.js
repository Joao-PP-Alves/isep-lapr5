import React, { createRef, useEffect } from 'react';
import renderGraph from './renderGraph';

export default function AppGraph() {
  const divRef = createRef();
  useEffect(() => renderGraph(divRef.current), [divRef]);
  return (
      <div ref={divRef} />
  );
}
