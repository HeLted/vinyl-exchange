import React, { Component } from "react";
import FilterReleasesComponent from "./FilterReleasesComponent";
import { Url, Controllers, Queries } from "./../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../contexts/NotificationContext";

class FilterReleaseContainer extends Component {
  constructor() {
    super();
    this.state = {
      genreSelectInput: "",
      styleMultiSelectInput: [],
      genres: [],
      styles: []
    }; 
  }
  static contextType = NotificationContext;

  componentDidMount() {
    axios
      .get(Url.api + Controllers.genres.name + Url.slash)
      .then(response => {
        this.context.handleAppNotification("Loaded Genres", 5);
        const genres = response.data;
        genres.unshift({id:"",name:"All"})
      
        this.setState({ genres: genres});
      })
      .catch(error => {
        this.context.handleServerNotification(error.response);
      });
  }

  componentDidUpdate(prevProps, prevState) {
    if (
      prevState.genreSelectInput !== this.state.genreSelectInput &&
      this.state.genreSelectInput !== "not selected"
    ) {
      axios
        .get(
          Url.api +
            Controllers.styles.name +
            Controllers.styles.actions.getAllStylesForGenre +
            Url.queryStart +
            Queries.genreId +
            Url.equal +
            this.state.genreSelectInput
        )
        .then(response => {
          this.context.handleAppNotification("Loaded styles for genre", 5);
          const styles = response.data.map(style => {
            return { value: style.id, label: style.name };
          });

          this.setState({ styles: styles });
        })
        .catch(error => {
          this.context.handleServerNotification(error.response);
        });
    }
  }

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
   
    this.setState({styleMultiSelectInput:[]})
    this.props.functions.onUpdateFilterValue([],value) 

  };

  handleOnChangeMultiSelect = value => {
    if (value == null) {
      this.props.functions.onUpdateFilterValue([]) 
      this.setState({ styleMultiSelectInput: [] });
    } else {
      const styleIds = value.map(styleObj => {
        return styleObj.value;
      });
  
      this.props.functions.onUpdateFilterValue(styleIds,this.state.genreSelectInput) 
      this.setState({ styleMultiSelectInput: value });
    }

    
  };

  render() {
    return (
      <FilterReleasesComponent
        data={{
          genres: this.state.genres,
          styles: this.state.styles,
          genreSelectInput: this.state.genreSelectInput,
          styleMultiSelectInput: this.state.styleMultiSelectInput
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnChangeMultiSelect: this.handleOnChangeMultiSelect
        }}
      />
    );
  }
}

export default FilterReleaseContainer;
