mergeInto(LibraryManager.library, {
  IsMobileBrowser: function () {
    return (typeof window.isMobileDevice !== 'undefined' && window.isMobileDevice) ? 1 : 0;
  }
});
