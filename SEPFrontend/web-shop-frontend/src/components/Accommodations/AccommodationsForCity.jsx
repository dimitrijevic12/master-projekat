import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getAccommodationsForCity } from "../../actions/actionsAccommodation";
import { getImagesForItems } from "../../actions/actionsItems";

class AccommodationsForCity extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
    city: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getAccommodationsForCity();
    await this.getAllImages(this.props.accommodationsForCity);
  }
  render() {
    if (this.props.accommodationsForCity === undefined) {
      return null;
    }

    debugger;
    const accommodationsForCity = this.props.accommodationsForCity;

    if (this.props.itemsImages === undefined) {
      return null;
    }

    if (
      this.props.itemsImages.length !== this.props.accommodationsForCity.length
    ) {
      return null;
    }

    debugger;
    const AccommodationsList = () => {
      debugger;
      return accommodationsForCity.map((post, i) =>
        this.props.itemsImages[i].contentType === "image/jpeg" ? (
          <Photo
            src={
              "data:image/jpg;base64," + this.props.itemsImages[i].fileContents
            }
            onClick={() => this.view(post)}
          />
        ) : (
          <video
            controls
            onClick={() => this.view(post)}
            style={{ width: 367, height: 370 }}
            className="mb-3"
          >
            <source
              src={
                "data:video/mp4;base64," +
                this.props.itemsImages[i].fileContents
              }
              type="video/mp4"
            ></source>
          </video>
        )
      );
    };

    return (
      <div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="tag">City:</label>
              <input
                type="text"
                name="city"
                value={this.state.city}
                onChange={this.handleChange}
                class="form-control"
                id="city"
                placeholder="Enter city"
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-40 pl-4">
              <br />
              <span style={{ width: 30, display: "inline-block" }}></span>
              <img
                onClick={() => {
                  this.search();
                }}
                src="/images/analytics.png"
              />
            </div>
          </div>
        </div>
        <br />
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>Accommodations</GridControlBarItem>
          </GridControlBar>
          <AccommodationsList />
        </Grid>
      </div>
    );
  }

  async search() {
    debugger;
    if (this.state.city === "") {
      await this.props.getAccommodationsForCity();
    } else {
      await this.props.getAccommodationsForCity(this.state.city);
    }

    await this.getAllImages(this.props.accommodationsForCity);
  }

  view(f) {
    localStorage.setItem("accommodation-id", f.id);
    window.location = "/accommodation/" + f.id;
  }

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };

  getAllImages = async (items) => {
    debugger;
    const paths = [];
    for (var i = 0; i < items.length; i++) {
      if (items[i].imagePath === undefined) {
        paths.push(items[i].imagePaths[0]);
      } else {
        paths.push(items[i].imagePath);
      }
    }
    await this.props.getImagesForItems(paths);
  };
}

const mapStateToProps = (state) => ({
  accommodationsForCity: state.accommodationsForCity,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getAccommodationsForCity,
    getImagesForItems,
  })
)(AccommodationsForCity);
