import React from 'react';
import Graph from './graph';

class AppGraphContent extends React.Component {

  
    constructor(props) {
      
        super(props);
        this.canvasRef = React.createRef();
    }

    // ******************* COMPONENT LIFECYCLE ******************* //
    componentDidMount() {
        // Get canvas, pass to custom class
        const canvas = this.canvasRef.current;

        this.graph = new Graph(canvas);

        // Init any event listeners
        window.addEventListener('mousemove', this.mouseMove);
        window.addEventListener('resize', this.handleResize);
    }

    componentDidUpdate(prevProps, prevState) {
        // Pass updated props to 
        const newValue = this.props.whateverProperty;
        this.Graph.updateValue(newValue);
    }


    // ******************* EVENT LISTENERS ******************* //
    mouseMove = (event) => {
        this.graph.onMouseMove();
    }

    handleResize = () => {
        this.graph.onWindowResize(window.innerWidth, window.innerHeight);
    };

    render() {
        return (
            <div className="canvasContainer">
                <canvas ref={this.canvasRef} />
            </div>
        );
    }
}

export default function AppGraph() {
  return <AppGraphContent />;
}
