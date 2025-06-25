using MarketToolsV3.PermissionStore.Application.Enums;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Domain.ValueObjects;

namespace MarketToolsV3.PermissionStore.Application.Utilities.Implementation;

public class PermissionSettingNodeBuilder : IPermissionSettingNodeBuilder
{
    private IReadOnlyList<PermissionSettingNodeViewModel> _nodes = [];
    private IPermissionsUtility? _permissionsUtility;
    private Dictionary<string, PermissionStatusEnum>? _pathAndStatusPairs;
    private Dictionary<string, bool>? _pathAndIsRequireStatusPairs;

    public IPermissionSettingNodeBuilder SetNodes(IEnumerable<PermissionNodeDto> nodes)
    {
        _nodes = ConvertNodes(nodes);

        return this;
    }

    public IPermissionSettingNodeBuilder SetViewNames(IPermissionsUtility permissionsUtility)
    {
        _permissionsUtility = permissionsUtility;

        return this;
    }

    public IPermissionSettingNodeBuilder SetStatuses(IEnumerable<PermissionSettingDto> permissions)
    {
        _pathAndStatusPairs = permissions.ToDictionary(
            x => x.Path,
            x => x.Status);

        return this;
    }

    public IPermissionSettingNodeBuilder SetRequireUseStatuses(IEnumerable<PermissionValueObject> permissions)
    {
        _pathAndIsRequireStatusPairs = permissions.ToDictionary(
            x => x.Path,
            x => x.RequireUse);

        return this;
    }

    public IEnumerable<PermissionSettingViewNodeDto> Build()
    {
        SetViews();
        SetStatuses();
        SetIsRequireStatuses();

        return _nodes.Select(ConvertNode);
    }

    private void SetViews()
    {
        if (_permissionsUtility is not null)
        {
            foreach (var node in _nodes)
            {
                SetViewName(node, _permissionsUtility);
            }
        }
    }

    private void SetStatuses()
    {
        if (_pathAndStatusPairs is not null)
        {
            foreach (var node in _nodes)
            {
                SetStatus(node, _pathAndStatusPairs);
            }
        }
    }

    private void SetIsRequireStatuses()
    {
        if (_pathAndIsRequireStatusPairs is not null)
        {
            foreach (var node in _nodes)
            {
                SetIsRequireStatus(node, _pathAndIsRequireStatusPairs);
            }
        }
    }

    private void SetIsRequireStatus(PermissionSettingNodeViewModel node, Dictionary<string, bool> pathAndIsRequireStatusPairs)
    {
        if (string.IsNullOrEmpty(node.Path) == false)
        {
            node.RequireUse = pathAndIsRequireStatusPairs.GetValueOrDefault(node.Path);
        }

        foreach (var nextNode in node.Nodes)
        {
            SetIsRequireStatus(nextNode, pathAndIsRequireStatusPairs);
        }
    }

    private void SetStatus(PermissionSettingNodeViewModel node, Dictionary<string, PermissionStatusEnum> pathAndStatusPairs)
    {
        if (string.IsNullOrEmpty(node.Path) == false)
        {
            node.Status = pathAndStatusPairs.GetValueOrDefault(node.Path);
        }

        foreach (var nextNode in node.Nodes)
        {
            SetStatus(nextNode, pathAndStatusPairs);
        }
    }

    private static PermissionSettingViewNodeDto ConvertNode(PermissionSettingNodeViewModel node)
    {
        if (node.Name is null) throw new ArgumentNullException(nameof(PermissionSettingNodeViewModel.Name));
        if (node.Path is null) throw new ArgumentNullException(nameof(PermissionSettingNodeViewModel.Path));

        return new PermissionSettingViewNodeDto(node.Name)
        {
            Path = node.Path,
            RequireUse = node.RequireUse,
            View = node.View ?? node.Name,
            Status = node.Status,
            Nodes = node.Nodes.Select(ConvertNode)
        };
    }

    private static void SetViewName(PermissionSettingNodeViewModel node, IPermissionsUtility permissionsUtility)
    {
        if (string.IsNullOrEmpty(node.Name) == false)
        {
            node.View = permissionsUtility.FindOrDefaultByNameView(node.Name);
        }

        foreach (var nextNode in node.Nodes)
        {
            SetViewName(nextNode, permissionsUtility);
        }
    }

    private static IReadOnlyList<PermissionSettingNodeViewModel> ConvertNodes(IEnumerable<PermissionNodeDto> nodes)
    {
        return nodes
            .Select(n => new PermissionSettingNodeViewModel
            {
                Name = n.Name,
                Nodes = ConvertNodes(n.Next)
            })
            .Select(n =>
            {
                if (string.IsNullOrEmpty(n.Name) == false)
                {
                    SetPath(n, n.Name);
                }
                return n;
            })
            .ToList();
    }

    private static void SetPath(PermissionSettingNodeViewModel node, string lastSegmentsGroup)
    {
        node.Path = $"{lastSegmentsGroup}:{node.Name}";

        foreach (var nextNode in node.Nodes)
        {
            SetPath(nextNode, node.Path);
        }
    }
}