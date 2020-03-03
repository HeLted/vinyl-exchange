import React,{Component} from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faInfo } from "@fortawesome/free-solid-svg-icons";

class InfoTooltip extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }
  render() {
    return (
      <span
        class="d-inline-block"
        tabindex="0"
        data-toggle="tooltip"
        title={this.props.data.tooltipValue}
      >
        <button
          class="btn btn-primary btn-lg"
          style={{"pointer-events": "none"}}
          type="button"
          disabled
        >
          <FontAwesomeIcon icon={faInfo} />
        </button>
      </span>
    );
  }
}

export default InfoTooltip;
