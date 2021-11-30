import React, { Component } from "react";
import CreateCourse from "../components/Courses/CreateCourse";
import Layout from "../layouts/Layout";

class CreateCoursePage extends Component {
  render() {
    return (
      <Layout>
        <CreateCourse />
      </Layout>
    );
  }
}

export default CreateCoursePage;
