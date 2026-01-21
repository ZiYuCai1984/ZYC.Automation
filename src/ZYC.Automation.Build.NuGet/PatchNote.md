
# 🚀 Release Notes - Version $(Version)

**Release Date:** $(ReleaseDate)

---

## 🌟 Highlights

✨ **Major refactor of NuGet module management and UI integration**

This release significantly improves the internal design and usability of NuGet-based modules, laying a more solid foundation for future automation workflows and module lifecycle management.

---

## 🆕 New Features

- Introduced a clearer NuGet module domain model:
  - `NuGetModuleState`
  - `NuGetModuleConfig`
  - `InstalledNuGetModule`
- Fully refactored **INuGetModuleManager** to an async-first API
- Enhanced **NuGetModuleManagerView** with:
  - Module list visualization
  - Install / uninstall actions
  - Manual refresh
  - NuGet cache cleanup

---

## 🛠 Improvements

- Removed legacy manifest / state / config coupling in favor of explicit domain objects
- Simplified module installation pipeline by removing file extraction responsibilities from the manager
- Improved UI–logic separation for module management workflows
- Upgraded dependency **ZYC.CoreToolkit** to **v3.7.4**

---

## 🐛 Bug Fixes

- Fixed typo in `ManagedTaskProgressChangedEvent` and updated all related usages

---

## 📦 Installation

```bash
dotnet add package ZYC.Automation.Alpha --version $(Version)
```

---

## 📚 Resources

* 📖 [Documentation](https://github.com/ZiYuCai1984/ZYC.Automation)
* 🐞 [Report an Issue](https://github.com/ZiYuCai1984/ZYC.Automation/issues)

---

**Thank you for trying out ZYC.Automation.Alpha!**
Your feedback will help shape future releases.

