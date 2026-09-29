# 项目配置说明

## 1. SuperMap iObjects .NET

本项目依赖 SuperMap iObjects .NET 2025。GitHub 仓库不包含 SuperMap 安装包、DLL、许可证或其他第三方运行库。

请在 `src/czx3/czx3.csproj` 中修改 `SuperMapIObjectsPath` 为本机实际安装目录。

## 2. SQL Server

项目包含 SQL Server 数据源连接示例。请在 `Form1.cs` 中将以下占位符替换为本机测试环境参数：

- `YOUR_SERVER`
- `YOUR_USERNAME`
- `YOUR_PASSWORD`
- 数据库名称

不要提交真实密码或其他敏感凭据。

## 3. 截图

将项目运行截图放入根目录 `screenshots/`，推荐使用以下命名：

- `01_属性查询.png`
- `02_GPS轨迹播放.png`
- `03_分段专题图.png`
- `04_点密度专题图.png`
- `05_统计专题图.png`
