
using Octokit;

namespace DeployManager.Services;

public class GitHubService
{
    private readonly GitHubClient _client;

    public GitHubService(string token)
    {
        _client = new GitHubClient(new ProductHeaderValue("DeployManager"));
        _client.Credentials = new Credentials(token);
    }

    public async Task<IReadOnlyList<Branch>> GetBranches(string owner, string repo)
    {
        return await _client.Repository.Branch.GetAll(owner, repo);
    }

    public async Task CreateTag(string owner, string repo, string tagName, string commitSha)
    {
        var tag = new NewTag
        {
            Tag = tagName,
            Message = $"Release {tagName}",
            Object = commitSha,
            Type = TaggedType.Commit
        };

        var createdTag = await _client.Git.Tag.Create(owner, repo, tag);

        var reference = new NewReference($"refs/tags/{tagName}", createdTag.Sha);
        await _client.Git.Reference.Create(owner, repo, reference);
    }
}
