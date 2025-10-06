using Assignment3.Utilities;
using System;
using Xunit;

namespace Assignment3TestSuite;


public class PartITests
{
    //////////////////////////////////////////////////////////
    /// 
    /// Testing UrlParser class
    /// 
    ////////////////////////////////////////////////////////// 

   
    

    



    //////////////////////////////////////////////////////////
    /// 
    /// Testing CategoryService class
    /// 
    //////////////////////////////////////////////////////////

    [Fact]
    public void CategoryService_GetAllCategories_ShouldReturnAllCategories()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var categories = categoryService.GetCategories();
        // Assert
        Assert.NotNull(categories);
        Assert.Equal(3, categories.Count);
    }

    [Fact]
    public void CategoryService_GetCategoryById_ShouldReturnCorrectCategory()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var category = categoryService.GetCategory(2);
        // Assert
        Assert.NotNull(category);
        Assert.Equal(2, category.Id);
        Assert.Equal("Condiments", category.Name);
    }

    [Fact]
    public void CategoryService_GetCategoryById_NonExistent()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var category = categoryService.GetCategory(-1);
        // Assert
        Assert.Null(category);
    }

    [Fact]
    public void CategoryService_UpdateCategory_ShouldUpdateSuccessfully()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var result = categoryService.UpdateCategory(1, "UpdatedName");
        var updatedCategory = categoryService.GetCategory(1);
        // Assert
        Assert.True(result);
        Assert.NotNull(updatedCategory);
        Assert.Equal("UpdatedName", updatedCategory.Name);
    }

    [Fact]
    public void CategoryService_UpdateCategory_NonExistent()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var result = categoryService.UpdateCategory(-1, "UpdatedName");
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CategoryService_DeleteCategory_ShouldDeleteSuccessfully()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var result = categoryService.DeleteCategory(3);
        var deletedCategory = categoryService.GetCategory(3);
        // Assert
        Assert.True(result);
        Assert.Null(deletedCategory);
    }

    [Fact]
    public void CategoryService_DeleteCategory_NonExistent()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var result = categoryService.DeleteCategory(-1);
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CategoryService_CreateCategory_ShouldCreateSuccessfully()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var result = categoryService.CreateCategory(4, "NewCategory");
        var newCategory = categoryService.GetCategory(4);
        // Assert
        Assert.True(result);
        Assert.NotNull(newCategory);
        Assert.Equal(4, newCategory.Id);
        Assert.Equal("NewCategory", newCategory.Name);
    }

    [Fact]
    public void CategoryService_CreateCategory_DuplicateId()
    {
        // Arrange
        var categoryService = new CategoryService();
        // Act
        var result = categoryService.CreateCategory(1, "DuplicateCategory");
        // Assert
        Assert.False(result);
    }

}


