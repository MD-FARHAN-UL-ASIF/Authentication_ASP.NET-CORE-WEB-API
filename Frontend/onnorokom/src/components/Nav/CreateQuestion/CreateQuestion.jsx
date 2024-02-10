import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './CreateQuestion.css';

const CreateQuestion = () => {
  const [title, setTitle] = useState('');
  const [body, setBody] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [loading, setLoading] = useState(false);
  const [categories, setCategories] = useState([]);
  const [createdDate, setCreatedDate] = useState(new Date().toISOString());

  const accessToken = sessionStorage.getItem('accessToken');
  const userId = sessionStorage.getItem('userId');

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await axios.get('https://localhost:7002/api/Category', {
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${accessToken}`,
          },
        });

        setCategories(response.data.result);
      } catch (error) {
        console.error('Error fetching categories:', error.message);
      }
    };

    fetchCategories();
  }, [accessToken]);

  const handleCreateQuestion = async () => {
    setLoading(true);
    console.log({
         title,
         body,
         createdDate,
         categoryId,
        userId,
      });
    try {
      const response = await axios.post(
        'https://localhost:7002/api/Question',
        {
          Title: title,
          Body: body,
          CreatedDate: createdDate,
          CategoryId: categoryId,
          UserId: userId,
        },
        {
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );

      console.log('Create Question Response:', response);

      if (response.status === 500) {
        console.error('Internal Server Error:', response.statusText);
        alert('Question creation failed. Internal Server Error. Please try again later.');
      } else if (Object.keys(response.data).length === 0) {
        console.warn('Question creation failed:', response.statusText);
        alert('Question creation failed. Please check the provided data.');
      } else {
        alert('Question created successfully');
        // Handle success as needed
      }
    } catch (error) {
      console.error('Question Creation Failed due to:', error.message);
      alert('Question Creation Failed due to:' + error.message);

      // Log the entire error response
      if (error.response) {
        console.error('Error Response:', error.response);

        // Log validation errors if available
        if (error.response.data && error.response.data.errors) {
          console.error('Validation Errors:', error.response.data.errors);
        }
      }
    }

    setLoading(false);
  };

  return (
    <div className="create-question-container">
      <h2>Create Question</h2>
      <label>Title:</label>
      <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />

      <label>Body:</label>
      <textarea value={body} onChange={(e) => setBody(e.target.value)} />

      <label>Select Category:</label>
      <select onChange={(e) => setCategoryId(e.target.value)}>
        <option value="">Select Category</option>
        {categories.map((category) => (
          <option key={category.id} value={category.id}>
            {category.name}
          </option>
        ))}
      </select>

      <button onClick={handleCreateQuestion} disabled={loading}>
        Create Question
      </button>
    </div>
  );
};

export default CreateQuestion;
